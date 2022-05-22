using MediatR;

namespace HP.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        public string Type { get; }
        //public string EventType { get; }
        public string EntityId { get; }
        public string EntityType { get; }
        DateTime OccuredOn { get; }
    }
}
