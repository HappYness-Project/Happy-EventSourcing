using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(DomainEventBase eventModel);
        Task<List<DomainEventBase>> FindByAggregateId(Guid aggregateId);
    }
}
