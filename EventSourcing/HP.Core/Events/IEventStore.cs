using HP.Core.Models;

namespace HP.Core.Events
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, IReadOnlyCollection<DomainEvent> events, int expectedVersion);
        Task<List<DomainEvent>> GetEventsAsync(Guid aggregateId);
        Task<List<Guid>> GetAggregateIdAsync();
    }
}
