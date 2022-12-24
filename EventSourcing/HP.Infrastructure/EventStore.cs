using HP.Core.Events;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
//https://github.com/bolicd/eventstore/blob/1fd6faa1b4751d83e065c3df32c7a4a8b0e5ef7b/Infrastructure/Repositories/EventStoreRepository.cs
namespace HP.Infrastructure
{
    public class EventStore : IEventStore
    {
        private string EventStoreTableName = "EventStore";
        private readonly IMongoDbContext _mongoDbContext;
        private IEventProducer _eventProducer;
        public EventStore(IMongoDbContext mongoDbContext, IEventProducer eventProducer)
        {
            _mongoDbContext = mongoDbContext;
            _eventProducer = eventProducer;
        }
        public void Save<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            collection.InsertOne(@event);
        }
        public async Task SaveAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            await collection.InsertOneAsync(@event);
        }
        public async Task<IReadOnlyCollection<TDomainEvent>> GetEventsAsync<TDomainEvent>(int aggregateId) where TDomainEvent : IDomainEvent
        {
            var events = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            //collection.FindAsync(aggregateId);
            return null;
        }
        public async Task SaveEventsAsync(string aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName)
        {
            if (events.Count == 0) return;
            var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            //var eventStream = a
            await collection.InsertManyAsync(events);
        }
    }
}
