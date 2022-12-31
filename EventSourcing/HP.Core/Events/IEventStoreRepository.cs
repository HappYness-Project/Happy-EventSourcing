using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(IDomainEvent eventModel);
        Task<List<IDomainEvent>> FindByAggregateId(Guid aggregateId);
        Task<List<IDomainEvent>> FindAllAsync();
    }
}
