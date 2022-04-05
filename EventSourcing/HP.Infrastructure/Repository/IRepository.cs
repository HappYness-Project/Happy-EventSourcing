using Microsoft.EntityFrameworkCore.Storage;

namespace HP.Infrastructure
{
    public interface IEventStore
    {
        void Init();
        public bool AppendEvent<TStream>(Guid streamId, object @event, long? expectedVersion = null) where TStream : notnull;
    }
    public interface IAggregate
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<object> DequeueUncommittedEvents();
    }
    public interface IRepository<T> where T : IAggregate
    {
        T Find(Guid id);
        void Add(T aggregate);
        void Update(T aggregate);
        void Delete(T aggregate);
    }
    public class Repository<T> : IRepository<T> where T : IAggregate
    {
        private readonly IEventStore eventStore;

        public Repository(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }
 
    }
}