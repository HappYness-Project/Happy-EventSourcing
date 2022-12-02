using MediatR;
namespace HP.Core.Notifications
{
    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }
    public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
    {
        TEventType DomainEvent { get; }
    }
}
