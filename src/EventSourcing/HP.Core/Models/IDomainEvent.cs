using MediatR;

namespace HP.Core.Models
{
    public interface IDomainEvent : INotification
    {
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        public string EventType { get; }
        DateTime OccuredOn { get; }
    }
}
