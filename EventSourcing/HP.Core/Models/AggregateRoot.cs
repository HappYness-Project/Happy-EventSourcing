namespace HP.Core.Models
{
    public abstract class AggregateRoot<T> : Entity, IAggregateRoot<T> 
    {
        public AggregateRoot() {}
        public AggregateRoot(string id) : base(id)
        {

        }
        private List<IDomainEvent> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }
        protected abstract void When(IDomainEvent @event);
    }
}
//An aggregate is a collection of one or more related entities (and possibly value objects). 
// Each Aggregate has a single root entity, referred to as the aggregate root.
// The aggregate root is responsible for controlling access to all of the members of its aggregate.
