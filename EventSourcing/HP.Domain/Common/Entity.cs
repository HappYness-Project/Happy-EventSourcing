using MongoDB.Bson;

namespace HP.Domain.Common
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        public DateTime CreatedDate { get; private set; }
        public long Version { get; private set; }

        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreatedDate = DateTime.Now;
        }

        private List<IDomainEvent> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            // TODO : Check Invriants??
            // TODO L: Update Aggregate?? 
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        public void RemoveEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
        protected abstract void When(IDomainEvent @event);
    }
}
