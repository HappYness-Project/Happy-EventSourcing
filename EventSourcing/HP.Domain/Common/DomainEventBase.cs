using HP.Domain;
namespace HP.Domain.Common
{
    public abstract class DomainEventBase : IDomainEvent
    {
        public DomainEventBase(string entityId, string entityType)
        {
            EntityId = entityId;
            OccuredOn = DateTime.Now;
            EntityType = entityType;
            EventType = this.GetType().Name;
        }
        public DateTime OccuredOn { get; }
        public string EntityId { get; }
        public string EntityType { get; }
        public string EventType { get; }
        public int AggregateId { get; private set; }
        public int AggregateVersion { get; private set; }
        public string EventId { get; private set; }
        public string EventName { get; private set; }
        public string EventData { get; private set; }
    }
}

