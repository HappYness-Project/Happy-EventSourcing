using Confluent.Kafka;
using EventStore.Client;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Exceptions;
using HP.Core.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Net;
using System.Text.Json;
using System.Threading;


namespace HP.Infrastructure
{
    public class EventStore : IEventStore
    {
        protected readonly IMongoCollection<EventModel> _esCollection;
        private readonly IEventProducer _eventProducer;
        private readonly EventStoreClient _esClient;
        public EventStore(IMongoDbContext dbContext, IEventProducer eventProducer)
        {
            var settings = EventStoreClientSettings.Create("{connectionString}");
            _esClient = new EventStoreClient(settings);

            _esCollection = dbContext.GetCollection<EventModel>("") ?? throw new ArgumentNullException(nameof(dbContext));
            _eventProducer = eventProducer;
        }
        public async Task<List<Guid>> GetAggregateIdAsync()
        {
            var eventStream = await _esCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
            if (eventStream == null || !eventStream.Any())
                throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!.");

            return eventStream.Select(x => x.AggregateIdentifier).Distinct().ToList();
        }
        public async Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId)
        {
            var result = _esClient.ReadStreamAsync(Direction.Forwards, "some-stream",StreamPosition.Start);
            var events = await result.ToListAsync();

            var eventStream = await _esCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);
            if (eventStream == null || !eventStream.Any())
                throw new AggregateNotFoundException("Incorrect post ID provided.");

            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventsAsync(Guid aggregateId, string aggregateType, IReadOnlyCollection<IDomainEvent> events,int expectedVersion)
        {

            var eventStream = await _esCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);
            if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
                throw new ConcurrencyException();

            var version = expectedVersion;
            foreach (var @event in events)
            {

                version++;
                @event.AggregateVersion = version;
                // new code
                var eventData = new EventData(
                    Uuid.NewUuid(),
                    "TestEvent",
                    JsonSerializer.SerializeToUtf8Bytes(@event)
                );
                /////
                var eventModel = new EventModel
                {
                    TimeStamp = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    AggregateType = aggregateType,
                    EventType = @event.EventType,
                    EventData = @event
                };
                await _esCollection.InsertOneAsync(eventModel).ConfigureAwait(false);

                // new code
                await _esClient.AppendToStreamAsync(
                    "temp-stream-" + aggregateId,
                    StreamState.Any,
                    new[] { eventData }
                );
                // new code 
                await _eventProducer.ProducerAsync(@event);
            }
        }
    }
}
