using MongoDB.Bson;

namespace HP.Domain.Common
{
    public abstract class Entity : BaseEntity
    {
        public DateTime CreatedDate { get; private set; }
        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreatedDate = DateTime.Now;
        }

        private List<IDomainEvent> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
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
