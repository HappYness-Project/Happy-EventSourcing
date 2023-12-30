using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventStore
    {
        Task SaveEventsAsync(string streamId, IReadOnlyCollection<IDomainEvent> events,int expectedVersion);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(string streamId);
        Task<List<string>> GetAllAggregateIdsAsync();

        // AddSnapshot(ISanpshot snaptshot)
        // AddProjection(IProjection projection)
        // FindLastSnapshotAsync(AggregateId, MaxVersion,,~~)

    }
}
