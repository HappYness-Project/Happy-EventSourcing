using HP.Core.Models;

namespace HP.Core.Handlers
{
    public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(IAggregateRoot aggregate);
        Task<T> GetByIdAsync(Guid aggregateId);
    }

}
