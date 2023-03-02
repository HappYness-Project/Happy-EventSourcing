namespace HP.Core.Models
{
    public interface IAggregateRoot : IEntity
    {
        int Version { get; }
        public DateTime? Created { get; }
        IReadOnlyCollection<IDomainEvent> UncommittedEvents { get; }
        //void ReplayEvents(IEnumerable<IDomainEvent> @events);
        void AddDomainEvent(IDomainEvent domainEvent);
    }
}
