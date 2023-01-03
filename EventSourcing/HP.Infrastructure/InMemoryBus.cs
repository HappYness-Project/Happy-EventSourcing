using HP.Core.Events;
using MediatR;
namespace HP.Infrastructure
{
    public class InMemoryBus : IInMemoryBus
    {
        private readonly IMediator _mediator;
        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            await _mediator.Publish(notification);  
        }
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            return await _mediator.Send(request);
        }
    }
}
