using MediatR;

namespace HP.Application.Bus
{
    public interface IInMemoryBus
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
        Task Publish<TNotification>(TNotification notification) where TNotification : INotification;
    }
}
