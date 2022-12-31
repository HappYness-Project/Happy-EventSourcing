using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventProducer
    {
        Task ProducerAsync<T>(string topic, T @event) where T : IDomainEvent;
    }
}
