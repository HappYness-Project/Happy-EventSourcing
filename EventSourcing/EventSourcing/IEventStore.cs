using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface IEventStore
    {
        //void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        //List<Event> GetEventsForAggregate(Guid aggregateId);

        // Task SaveAsync(IentityId aggregateId, int originatigngVersion, IReadonlyCollection<IDomainEvent> events, string aggregateName = "AggregateName"))
       // Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId AggregateRootId);
    }
}
