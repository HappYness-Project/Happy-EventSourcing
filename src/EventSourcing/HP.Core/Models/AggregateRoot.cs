namespace HP.Core.Models
{
    /* An aggregate is a collection of one or more related entities (and possibly value objects). 
    Each Aggregate has a single root entity, referred to as the aggregate root.
    The aggregate root is responsible for controlling access to all of the members of its aggregate. */
    public abstract class AggregateRoot : BaseEntity, IAggregateRoot
    {
        public AggregateRoot() {
            this.Id = Guid.NewGuid();
            this.Created = DateTime.UtcNow;
        }
        private List<IDomainEvent> _domainEvents;
        public int Version { get; set; }
        public IReadOnlyCollection<IDomainEvent> UncommittedEvents => _domainEvents?.AsReadOnly();
        public DateTime? Created { get; }
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            domainEvent.AggregateId = this.Id;
            _domainEvents.Add(domainEvent);
            this.When(domainEvent);
            this.Version++;
        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        public abstract void When(IDomainEvent @event);

        public void ClearEvents()
        {
            _domainEvents?.Clear();
        }
    }
}

