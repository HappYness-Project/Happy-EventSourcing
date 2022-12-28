using HP.Core.Events;
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
        public void Save<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            // var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            // collection.InsertOne(@event);
        }
        public async Task SaveAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            // var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            // await collection.InsertOneAsync(@event);
            throw new NotImplementedException();
        }
        // public async Task<IReadOnlyCollection<T>> GetEventsAsync<T>(Guid aggregateId) where T : IDomainEvent
        // {
        //     var events = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
        //     //collection.FindAsync(aggregateId);
        //     return null;
        // }
        public async Task SaveEventsAsync(Guid aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events)
        {
            // if (events.Count == 0) return;
            // var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            // //var eventStream = a
            // var eventStream = await _eventStoreRepo.FindByAggregateId(aggregateId);        
            // await collection.InsertManyAsync(events);
            throw new NotImplementedException();
        }
        public async Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId)
        {
            throw new NotImplementedException();
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
