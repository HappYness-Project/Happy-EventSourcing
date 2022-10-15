using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Kafka
{
    public interface IEventProducer
    {
        Task ProducerAsync<T>(string topic, T @event) where T : DomainEventBase;
    }
}
