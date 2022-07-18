using HP.Domain;
namespace HP.Domain.Common
{
    public abstract class DomainEventBase : IDomainEvent
    {

        protected DomainEventBase() { }

        public DomainEventBase(string entityType)
        {
            EventId = Guid.NewGuid().ToString();
            OccuredOn = DateTime.Now;
            EntityType = entityType;
            EventType = this.GetType().Name;
        }
        public DateTime OccuredOn { get; }
        public string EntityType { get; }
        public string EventId { get; }
        public string EventType { get; }
        public int AggregateId { get; private set; }
        public int AggregateVersion { get; private set; }
        public string EventData { get; private set; }
    }
}

