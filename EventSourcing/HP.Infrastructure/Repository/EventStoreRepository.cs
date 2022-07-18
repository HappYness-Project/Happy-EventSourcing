using HP.Domain;
using HP.Domain.Common;
using HP.Infrastructure.DbAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//https://github.com/bolicd/eventstore/blob/1fd6faa1b4751d83e065c3df32c7a4a8b0e5ef7b/Infrastructure/Repositories/EventStoreRepository.cs
namespace HP.Infrastructure.Repository
{
    public class EventStoreRepository : IEventStore
    {
        private string EventStoreTableName = "EventStore";
        private readonly IConfiguration _configuration;
        private readonly IMongoDbContext _mongoDbContext;
        public EventStoreRepository(IConfiguration configuration, IMongoDbContext mongoDbContext)
        {
            _configuration = configuration;
            _mongoDbContext = mongoDbContext;   
            // check if the Event Store for the Evet exists in the Mongo DB?
            // Creating Mongo DB - database if there are any.
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

        public Task<IReadOnlyCollection<TDomainEvent>> GetEvents<TDomainEvent>(int aggregateId) where TDomainEvent : IDomainEvent
        {
            var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            //collection.FindAsync(aggregateId);
            return null;
        }

        public async Task SaveEventsAsync(string aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName)
        {
            if (events.Count == 0) return;
            var collection = _mongoDbContext.GetCollection<IDomainEvent>(EventStoreTableName);
            await collection.InsertManyAsync(events);
        }
    }
}
