using HP.Core.Events;
using HP.Core.Models;
namespace HP.Application.Commands
{
    public abstract class BaseCommandHandler
    {
        private readonly IEventProducer _eventProducer;
        protected BaseCommandHandler(IEventProducer eventProducer)
        {
            this._eventProducer = eventProducer ?? throw new ArgumentNullException(nameof(eventProducer));
        }
        protected async Task ProduceDomainEvents(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            foreach(var domainEvent in domainEvents)
                await _eventProducer.ProducerAsync(domainEvent);
        }
    }
}
