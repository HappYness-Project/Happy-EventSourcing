using HP.Domain.Common;
using HP.Infrastructure.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository
{
    public class EventStoreRepository : IEventStore
    {
        // Connection factory for NoSQL seems appropriate .
        private string EventStoreTableName = "EventStore";
        public async Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(string aggregateRootId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(string aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName = "Aggregate Name")
        {
            if (events.Count == 0) return;

            var query = "Insert query";

            var listofEvents = events.Select(e => new
            {
                Aggregate 
            })

            throw new NotImplementedException();
        }
    }
}
