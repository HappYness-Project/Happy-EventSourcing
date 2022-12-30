namespace HP.Core.Models
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase() 
        { 
            EventId = Guid.NewGuid();
            OccuredOn = DateTime.Now;
            EventType = this.GetType().Name;
        }
        public Guid EventId { get; }
        public Guid AggregateId { get; set; }
        public DateTime OccuredOn { get; }
        public string EventType { get; }
        public int AggregateVersion { get; set; }
        public EventData EventData { get; set; }
    }
}

