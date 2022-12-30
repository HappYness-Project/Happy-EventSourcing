using HP.Core.Events;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
namespace HP.Infrastructure.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<DomainEventBase> _eventStoreCollection;
        public EventStoreRepository(IMongoDbContext mongoDbContext)
        {
            _eventStoreCollection = mongoDbContext.GetCollection<DomainEventBase>("HP.EventStore");
        }

        public async Task<List<DomainEventBase>> FindAllAsync()
        {
            return await _eventStoreCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<DomainEventBase>> FindByAggregateId(Guid aggregateId)
        {
            return await _eventStoreCollection.Find(x => x.AggregateId == aggregateId).ToListAsync().ConfigureAwait(false); 
        }
        public async Task SaveAsync(DomainEventBase @event)
        {
            await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
        }
    }
}
