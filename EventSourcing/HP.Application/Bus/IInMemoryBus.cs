using HP.Application.IntegrationEvents;
using MediatR;

namespace HP.Application.Bus
{
    public interface IInMemoryBus
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
        Task Publish<TNotification>(TNotification notification) where TNotification : INotification;
    }
    public interface IEventProducer
    {
        Task DispatchAsync(IIntegrationEvent @event, CancellationToken cancellation = default);
    }
    public interface IEventConsumer
    {
        Task StartConsumeAsync(CancellationToken cancellation = default);
        event EventReceivedHandler EventReceived;
        event ExceptionThrownHandler ExceptionThrown;
    }
    public delegate Task EventReceivedHandler(object sender, IIntegrationEvent @event);
    public delegate void ExceptionThrownHandler(object sender, Exception exception);
}


// Use this code https://github.com/EduardoPires/EquinoxProject/blob/master/src/Equinox.Infra.CrossCutting.Bus/InMemoryBus.cs


