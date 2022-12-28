using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, int originatingVersion, IReadOnlyCollection<DomainEventBase> events);   
        Task<List<EventData>> GetEventsAsync(Guid aggregateId);
        Task<List<Guid>> GetAggregateIdAsync();
    }
}
