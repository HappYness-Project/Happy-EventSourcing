using HP.Core.Events;
using HP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Commands
{
    public abstract class BaseCommandHandler
    {
        private readonly IEventProducer _eventProducer;
        protected BaseCommandHandler(IEventProducer eventProducer)
        {
            _eventProducer = eventProducer;
        }
        protected async Task ProduceDomainEvents(string topic, IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            foreach(var domainEvent in domainEvents)
                await _eventProducer.ProducerAsync(topic, domainEvent);
        }
    }
}
