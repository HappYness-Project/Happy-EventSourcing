using Confluent.Kafka;
using EventStore.Client;
using Grpc.Core;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Exceptions;
using HP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MongoDB.Driver;
using System.Net;
using System.Text.Json;
using System.Threading;


namespace HP.Infrastructure
{
    public class EventStore : IEventStore
    {
        private readonly IEventProducer _eventProducer;
        private readonly IEventSerializer _eventSerializer;
        private readonly EventStoreClient _esClient;
        public EventStore(EventStoreClient client, IEventProducer eventProducer)
        {
            _esClient = client;
            _eventProducer = eventProducer;
        }
        public async Task<List<string>> GetAllAggregateIdsAsync()
        {
            var eventStream = await _esClient.ReadAllAsync(Direction.Forwards, Position.Start).ToListAsync().ConfigureAwait(false);
            if (eventStream == null || !eventStream.Any())
                throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!.");

            return eventStream.Select(x => x.OriginalStreamId).Distinct().ToList();

        }
        public async Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId, string streamName)
        {
            var result = _esClient.ReadStreamAsync(Direction.Forwards, streamName + "-" + aggregateId, StreamPosition.Start);
            List<ResolvedEvent> events = await result.ToListAsync().ConfigureAwait(false);
            if(events ==null|| !events.Any())
                throw new AggregateNotFoundException("Incorrect stream ID provided.");

            var deserializedEvents = events.Select(Map);
            return deserializedEvents;
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
                var eventData = new EventData(Uuid.NewUuid(),"TestEvent",JsonSerializer.SerializeToUtf8Bytes(@event));
                await _esClient.AppendToStreamAsync($"{aggregateType}-" + aggregateId,StreamState.Any,new[] { eventData });
                // TODO: Should we append in the same event stream?????
                await _eventProducer.ProducerAsync(@event);
            }
        }

        private IDomainEvent Map(ResolvedEvent resolvedEvent)
        {
            var meta = JsonSerializer.Deserialize<EventMeta>(resolvedEvent.Event.Metadata.ToArray());
            return _eventSerializer.Deserialize(meta.EventType, resolvedEvent.Event.Data.ToArray());
        }
        internal struct EventMeta
        {
            public string EventType { get; set; }
        }
    }
}
