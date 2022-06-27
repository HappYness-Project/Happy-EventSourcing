using HP.Domain;
using MediatR;
using System;

namespace HP.Infrastructure
{
    public class InMemoryBus : IInMemoryBus
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;
        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public async Task Publish<TNotification>(TNotification notification) where TNotification : INotification
        {

            //Convertion might required????
            //_eventStore?.Save(notification);
            await _mediator.Publish(notification);  
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            return await _mediator.Send(request);
        }
    }
// Use this code https://github.com/EduardoPires/EquinoxProject/blob/master/src/Equinox.Infra.CrossCutting.Bus/InMemoryBus.cs
}
