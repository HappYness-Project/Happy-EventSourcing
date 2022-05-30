using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.EventStore
{
    public interface IEventStore
    {
        Task SaveAsync(string aggregateId,
            int originatingVersion, // tUsed in optimistic concurrency check.
            IReadOnlyCollection<IDomainEvent> events,// Actual list of events that needs to be persisted into db. Each event is persisted as new row. 
            string aggregateName = "Aggregate Name");
        // Used to persist aggregate as stream of events.
        //The aggregate itself is described as a collection of domain events, with a uinque name.

        Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(string aggregateRootId);
        // fetches aggregate, using aggregateId as param,....load the aggregate.
    }

}
