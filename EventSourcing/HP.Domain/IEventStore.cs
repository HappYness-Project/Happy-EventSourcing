using HP.Domain.Common;

namespace HP.Domain
{
    public interface IEventStore
    {
        /// <summary>
        /// 
        /// </summary>
        void Save<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// 
        /// </summary>
        Task SaveAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// 
        /// </summary>
        //Task<List<TStoredEvent>> GetListAsync<TStoredEvent>(string entityId, string entityType) where TStoredEvent : StoredEvent;
        Task<IList<TDomainEvent>> GetEvents<TDomainEvent>(int aggregateId) where TDomainEvent : IDomainEvent;


    //    Task SaveAsync(string aggregateId,
    //int originatingVersion, // tUsed in optimistic concurrency check.
    //IReadOnlyCollection<IDomainEvent> events,// Actual list of events that needs to be persisted into db. Each event is persisted as new row. 
    //string aggregateName = "Aggregate Name");
    //    // Used to persist aggregate as stream of events.
    //    //The aggregate itself is described as a collection of domain events, with a uinque name.

    //    Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(string aggregateRootId);
        // fetches aggregate, using aggregateId as param,....load the aggregate.
    }
}
