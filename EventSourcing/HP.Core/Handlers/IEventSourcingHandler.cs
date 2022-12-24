using MediatR;

namespace HP.Core.Handlers
{
        public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(AggregateRoot aggregate);
        Task<T> GetByIdAsync(Guid aggregateId);
    }

}
