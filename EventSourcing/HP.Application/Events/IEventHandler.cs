using MediatR;

namespace HP.Application.Events
{
    public interface IEvent : INotification { }

    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event, CancellationToken ct);
    }
}
