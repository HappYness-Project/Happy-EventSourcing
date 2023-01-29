using System.Text.Json.Serialization;

namespace HP.Core.Models
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            EventId = Guid.NewGuid();
            OccuredOn = DateTime.Now;
            EventType = this.GetType().Name;
        }
        public Guid AggregateId { get; }
        public Guid EventId { get; }
        public DateTime OccuredOn { get; }
        public string EventType { get; }
        public int AggregateVersion { get; set; }
    }
}

