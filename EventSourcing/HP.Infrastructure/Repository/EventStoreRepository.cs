using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
namespace HP.Infrastructure.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<EventModel> _esCollection;
        public EventStoreRepository(IMongoDbContext dbContext)
        {
            _esCollection = dbContext.GetCollection<EventModel>() ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<EventModel>> FindAllAsync()
        {
            return await _esCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
        {
            return await _esCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);
        }

        public async Task SaveAsync(EventModel @event)
        {
            await _esCollection.InsertOneAsync(@event).ConfigureAwait(false);
        }
    }
}
