using HP.Core.Models;

namespace HP.Core.Events
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, string aggregateType, IReadOnlyCollection<IDomainEvent> events,int expectedVersion);
        Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId);
        Task<List<Guid>> GetAggregateIdAsync();

        // AddSnapshot(ISanpshot snaptshot)
        // AddProjection(IProjection projection)
        // FindLastSnapshotAsync(AggregateId, MaxVersion,,~~)

    }
}
