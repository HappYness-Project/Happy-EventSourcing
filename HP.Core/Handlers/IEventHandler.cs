using MediatR;

namespace HP.Core.Handlers
{
    public interface IEvent : INotification { }

    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event, CancellationToken ct);
    }
}
