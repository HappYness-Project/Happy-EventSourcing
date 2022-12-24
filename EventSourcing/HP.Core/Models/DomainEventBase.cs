namespace HP.Core.Models
{
    public abstract class DomainEventBase : IDomainEvent
    {
        protected DomainEventBase() { }
        public DomainEventBase(string entityType)
        {
            EventId = Guid.NewGuid();
            OccuredOn = DateTime.Now;
            EventType = this.GetType().Name;
        }
        public Guid EventId { get; }
        public Guid AggregateId { get; private set; }
        public DateTime OccuredOn { get; }
        public string EventType { get; }
        public int AggregateVersion { get; private set; }
        public string EventData { get; private set; }
    }
}

