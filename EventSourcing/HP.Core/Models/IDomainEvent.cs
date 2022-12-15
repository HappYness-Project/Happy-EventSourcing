using MediatR;
namespace HP.Core.Models
{
    public interface IDomainEvent : INotification
    {
        public Guid AggregateId { get; }
        public int AggregateVersion { get; }
        public string EventId { get; }
        public string EventType { get; }
        public string EventData { get; }
        DateTime OccuredOn { get; }
    }
}
