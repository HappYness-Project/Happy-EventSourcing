using MediatR;

namespace HP.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        public string EntityId { get; }
        public string EntityType { get; }
        public string EventType { get; }
        DateTime OccuredOn { get; }
    }
}
