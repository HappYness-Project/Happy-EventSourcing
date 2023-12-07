using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, string aggregateType, IReadOnlyCollection<IDomainEvent> events,int expectedVersion);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId, string streamName);
        Task<List<string>> GetAllAggregateIdsAsync();

        // AddSnapshot(ISanpshot snaptshot)
        // AddProjection(IProjection projection)
        // FindLastSnapshotAsync(AggregateId, MaxVersion,,~~)

    }
}
