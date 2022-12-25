using HP.Core.Models;
using MediatR;

namespace HP.Core.Handlers
{
        public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(IAggregateRoot<T> aggregate);
        Task<T> GetByIdAsync(Guid aggregateId);
    }

}
