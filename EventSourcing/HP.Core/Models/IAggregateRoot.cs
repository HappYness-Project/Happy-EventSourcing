namespace HP.Core.Models
{
    public interface IAggregateRoot
    {
        int Version { get; }
        IReadOnlyCollection<IDomainEvent> UncommittedEvents { get; }
        //void ReplayEvents(IEnumerable<IDomainEvent> @events);
        void AddDomainEvent(IDomainEvent domainEvent);
        void RaiseEvents(IDomainEvent @event);
    }
}
