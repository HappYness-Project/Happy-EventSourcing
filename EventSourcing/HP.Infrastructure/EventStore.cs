using HP.Core.Events;
using HP.Core.Exceptions;
using HP.Core.Models;
using MongoDB.Driver;
namespace HP.Infrastructure
{
    public class EventStore : IEventStore
    {
        private readonly IMongoCollection<EventModel> _esCollection;
        private readonly IEventProducer _eventProducer;
        public EventStore(IMongoCollection<EventModel> esCollection, IEventProducer eventProducer)
        {
            _esCollection = esCollection;
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
            var eventStream = await _esCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);
            if (eventStream == null || !eventStream.Any())
                throw new AggregateNotFoundException("Incorrect post ID provided.");

            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventsAsync(Guid aggregateId, IReadOnlyCollection<IDomainEvent> events, int expectedVersion)
        {
            var eventStream = await _esCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);
            if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
                throw new ConcurrencyException();

            var version = expectedVersion;
            foreach (var @event in events)
            {
                version++;
                @event.AggregateVersion = version;
                var eventType = @event.GetType().Name;
                var eventModel = new EventModel
                {
                    TimeStamp = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    Version = version,
                    EventType = @event.EventType,
                    EventData = @event
                };
                await _esCollection.InsertOneAsync(eventModel).ConfigureAwait(false);
                await _eventProducer.ProducerAsync(@event);
            }
        }
    }
}
