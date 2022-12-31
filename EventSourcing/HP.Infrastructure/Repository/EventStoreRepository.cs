using HP.Core.Events;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
namespace HP.Infrastructure.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<IDomainEvent> _eventStoreCollection;
        public EventStoreRepository(IMongoDbContext mongoDbContext)
        {
            _eventStoreCollection = mongoDbContext.GetCollection<IDomainEvent>("HP.EventStore");
        }

        public async Task<List<IDomainEvent>> FindAllAsync()
        {
            return await _eventStoreCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<IDomainEvent>> FindByAggregateId(Guid aggregateId)
        {
            return await _eventStoreCollection.Find(x => x.AggregateId == aggregateId).ToListAsync().ConfigureAwait(false); 
        }
        public async Task SaveAsync(IDomainEvent @event)
        {
            await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
        }
    }
}
