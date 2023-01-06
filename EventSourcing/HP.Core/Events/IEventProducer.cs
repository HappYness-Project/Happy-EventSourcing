using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventProducer
    {
        Task ProducerAsync<T>(T @event) where T : IDomainEvent;
    }
}
