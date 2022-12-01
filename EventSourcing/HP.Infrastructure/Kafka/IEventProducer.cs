using HP.Domain.Common;
namespace HP.Infrastructure.Kafka
{
    public interface IEventProducer
    {
        Task ProducerAsync<T>(string topic, T @event) where T : DomainEventBase;
    }
}
