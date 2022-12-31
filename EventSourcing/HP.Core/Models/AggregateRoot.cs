namespace HP.Core.Models
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public AggregateRoot() { }
        private List<IDomainEvent> _domainEvents;
        public int Version { get; set; }
        public Guid Id { get; protected set; } = default!;
        public IReadOnlyCollection<IDomainEvent> UncommittedEvents => _domainEvents?.AsReadOnly();
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        private void ApplyChange(IDomainEvent @event, bool isNew)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), $"This Apply method was not found in the aggregate for {@event.GetType().Name}");
            }
            method.Invoke(this, new object[] { @event });
            if (isNew)
            {
                _domainEvents.Add(@event);
            }
        }
        public void RaiseEvents(IDomainEvent @event)
        {
            ApplyChange(@event, true);
        }
        public void ReplayEvents(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event, false);
            }
        }
        protected abstract void When(IDomainEvent @event);

        void IAggregateRoot<Guid>.AddDomainEvent(IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}
//An aggregate is a collection of one or more related entities (and possibly value objects). 
// Each Aggregate has a single root entity, referred to as the aggregate root.
// The aggregate root is responsible for controlling access to all of the members of its aggregate.
