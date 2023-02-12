using HP.Core.Events;
using HP.Core.Exceptions;
using HP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _esRepository;
        private readonly IEventProducer _eventProducer;
        public EventStore(IEventStoreRepository esRepository, IEventProducer eventProducer  
        {
            _esRepository = esRepository;
            _eventProducer = eventProducer;
        }
        public async Task<List<Guid>> GetAggregateIdAsync()
        {
            var eventStream = await _esRepository.FindAllAsync();
            if (eventStream == null || !eventStream.Any())
                throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!.");

            return eventStream.Select(x => x.AggregateIdentifier).Distinct().ToList();
        }

        public async Task<List<DomainEvent>> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepo.FindByAggregateId(aggregateId);
            if (eventStream == null || !eventStream.Any())
                throw new AggregateNotFoundException("Incorrect post ID provided.");

            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<DomainEvent> events, int expectedVersion)
        {
            var eventStream = await _eventStoreRepo.FindByAggregateId(aggregateId);

            if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
                throw new ConcurrencyException();

            var version = expectedVersion;
            foreach (var @event in events)
            {
                version++;
                @event.Version = version;
                var eventType = @event.GetType().Name;
                var eventModel = new EventModel
                {
                    TimeStamp = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    AggregateType = nameof(PostAggregate),
                    Version = version,
                    EventType = eventType,
                    EventData = @event
                };
                await _eventStoreRepo.SaveAsync(eventModel);

                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                await _eventProducer.ProducerAsync(topic, @event);
            }
    }
}
