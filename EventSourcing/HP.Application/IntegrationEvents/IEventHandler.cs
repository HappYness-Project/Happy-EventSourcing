using MediatR;

namespace HP.Application.IntegrationEvents
{
    public interface IEvent : INotification { }

    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event, CancellationToken ct);
    }
}
