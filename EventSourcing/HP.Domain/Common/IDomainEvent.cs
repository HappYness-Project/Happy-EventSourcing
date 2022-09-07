using MediatR;

namespace HP.Domain.Common
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
