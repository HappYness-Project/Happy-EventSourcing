using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
        Task<List<EventModel>> FindAllAsync();
    }
}
