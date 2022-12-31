namespace HP.Core.Models
{
    public interface IAggregateRoot : IAggregateRoot<Guid> { }
    public interface IAggregateRoot<out TKey>
    {
        TKey Id { get; }
        int Version { get; }
        IReadOnlyCollection<IDomainEvent> UncommittedEvents { get; }
        //void ReplayEvents(IEnumerable<IDomainEvent> @events);
        void AddDomainEvent(IDomainEvent domainEvent);
        void RaiseEvents(IDomainEvent @event);
    }
}
