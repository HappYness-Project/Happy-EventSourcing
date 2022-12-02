using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventStore
    {
        void Save<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;
        Task SaveAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;
        Task SaveEventsAsync(string aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName);   
        Task<IReadOnlyCollection<TDomainEvent>> GetEvents<TDomainEvent>(int aggregateId) where TDomainEvent : IDomainEvent;
    }
}
