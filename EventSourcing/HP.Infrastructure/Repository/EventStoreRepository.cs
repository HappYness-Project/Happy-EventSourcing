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
        // Connection factory for NoSQL seems appropriate .
        private string EventStoreTableName = "EventStore";
        private static string EventStoreInsertColumns = "[Id], [CreatedAt], [Version], [Name], [AggregateId], [Data], [Aggregate]";

        private readonly IConfiguration _configuration;
        private readonly IMongoDbContext _mongoDbContext;
        // Mongo DB information 
        public EventStoreRepository(IConfiguration configuration, IMongoDbContext mongoDbContext)
        {
            _configuration = configuration;
            _mongoDbContext = mongoDbContext;   
            // check if the Event Store for the Evet exists in the Mongo DB?


            // Creating Mongo DB - database if there are any.
        }
        public async Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(int aggregateRootId)
        {
            if (aggregateRootId == null) throw new Exception("Cannot be null");

            throw new NotImplementedException();
        }

        //public async Task SaveAsync(string aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName = "Aggregate Name")
        //{
        //    if (events.Count == 0) return;

        //    var query = "Insert query";

        //    var listofEvents = events.Select(e => new
        //    {
        //        //Aggregate 
        //        aggregateId = aggregateId
        //    });

        //    throw new NotImplementedException();
        //}

        public void Save<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            throw new NotImplementedException();
        }

        public Task<IList<TDomainEvent>> GetEvents<TDomainEvent>(int aggregateId) where TDomainEvent : IDomainEvent
        {
            throw new NotImplementedException();
        }
    }
}
