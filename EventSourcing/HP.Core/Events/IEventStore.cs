using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventStore
    {
        void Save<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;
        Task SaveAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;
        Task SaveEventsAsync(Guid aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events);   
        // Task<IReadOnlyCollection<T>> GetEventsAsync<T>(Guid aggregateId) where T : IDomainEvent;
        Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId);
        Task<List<Guid>> GetAggregateIdAsync();
    }
}
