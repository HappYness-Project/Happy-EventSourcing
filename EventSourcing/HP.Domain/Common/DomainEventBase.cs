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
    }
}

