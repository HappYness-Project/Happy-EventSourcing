namespace HP.Core.Models
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            OccuredOn = DateTime.UtcNow;
            EventType = this.GetType().Name;
        }
        public Guid AggregateId { get; set; }
        public DateTime OccuredOn { get; }
        public string EventType { get; }
        public int AggregateVersion { get; set; }
    }
}

