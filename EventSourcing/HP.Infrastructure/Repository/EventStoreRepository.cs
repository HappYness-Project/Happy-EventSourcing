using HP.Core.Events;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<DomainEventBase> _eventStoreCollection;
        public EventStoreRepository(IMongoDbContext mongoDbContext)
        {
            _eventStoreCollection = mongoDbContext.GetCollection<DomainEventBase>("HP.EventStore");
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
