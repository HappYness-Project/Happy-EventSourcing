using HP.Core.Events;
using HP.Core.Exceptions;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
//https://github.com/bolicd/eventstore/blob/1fd6faa1b4751d83e065c3df32c7a4a8b0e5ef7b/Infrastructure/Repositories/EventStoreRepository.cs
namespace HP.Infrastructure
{
    public class EventStore : IEventStore
    {
        private string EventStoreTableName = "EventStore";
        private readonly IEventStoreRepository _esRepository;
        private IEventProducer _eventProducer;
        public EventStore(IEventStoreRepository repository, IEventProducer eventProducer)
        {
            _esRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventProducer = eventProducer ?? throw new ArgumentNullException(nameof(eventProducer));
        }
        public async Task SaveEventsAsync(Guid aggregateId, int originatingVersion, IReadOnlyCollection<DomainEventBase> events, int expectedVersion)
        {
            var eventStream = await _esRepository.FindByAggregateId(aggregateId);
            if(expectedVersion != -1 && eventStream[^1].AggregateVersion != expectedVersion)
                throw new ConcurrencyException();

            var version = expectedVersion;
            foreach(var @event in events){
                version++;
                @event.AggregateVersion = version;
                
                var eventMoel = new DomainEventBase 
                {
                    AggregateId = aggregateId,


                };
                _esRepository.SaveAsync(eventMoel);

            }

            // if (events.Count == 0) return;
            // var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            // //var eventStream = a
            // var eventStream = await _eventStoreRepo.FindByAggregateId(aggregateId);        
            // await collection.InsertManyAsync(events);
            throw new NotImplementedException();
        }
        public async Task<List<EventData>> GetEventsAsync(Guid aggregateId)
        {
            List<DomainEventBase> eventStream = await _esRepository.FindByAggregateId(aggregateId);
            if(eventStream == null || !eventStream.Any()) 
                throw new AggregateNotFoundException("Incorrect post ID provided.");

            return eventStream.OrderBy(x => x.AggregateVersion).Select(x => x.EventData).ToList();
        }
        public async Task<List<Guid>> GetAggregateIdAsync()
        {
            var eventStream = await _esRepository.FindAllAsync();
            if (eventStream == null || !eventStream.Any())
                throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!.");

            return eventStream.Select(x => x.AggregateId).Distinct().ToList();
        }
    }
}
