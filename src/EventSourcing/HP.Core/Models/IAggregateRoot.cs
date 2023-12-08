namespace HP.Core.Models
{
    public interface IAggregateRoot : IEntity
    {
        int Version { get; }
        public DateTime? Created { get; }
        IReadOnlyCollection<IDomainEvent> UncommittedEvents { get; }
        void AddDomainEvent(IDomainEvent domainEvent);
        void ClearEvents();
        //void ReplayEvents(IEnumerable<IDomainEvent> @events);
    }
}
