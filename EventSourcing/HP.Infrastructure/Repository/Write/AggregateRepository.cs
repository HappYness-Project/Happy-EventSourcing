using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using MongoDB.Driver;

namespace HP.Infrastructure.Repository.Write
{
    public class AggregateRepository<T> : IAggregateRepository<T> where T : IAggregateRoot
    {
        private readonly IEventStore _eventStore;
        private readonly string _StreamBase;
        public AggregateRepository(IEventStore eventStore)
        {
            var aggregateType = typeof(T);
            _StreamBase = aggregateType.Name;
            _eventStore = eventStore;
        }
        public async Task<List<Guid>> GetAggregateIdAsync()
        {
            var eventStream = await _eventStore.GetAggregateIdAsync();
            if (eventStream == null || !eventStream.Any())
                throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!.");

            return eventStream;
        }

        public async Task<T> GetByAggregateId<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot, new()
        {
             var events = await _eventStore.GetEventsAsync(id);
            if (events == null || !events.Any()) return null;
            T root = new T();
            foreach (var eve in events)
                root.When(eve);
            return (T)root;
        }

        public async Task PersistAsync(T aggregateRoot, CancellationToken ct = default)
        {
            if(aggregateRoot == null)
                throw new ArgumentNullException(nameof(aggregateRoot));

            if (!aggregateRoot.UncommittedEvents.Any())
                return;

            var aggregateType = typeof(T).Name;
            var firstEvent = aggregateRoot.UncommittedEvents.First();
            var version = firstEvent.AggregateVersion - 1;
            await _eventStore.SaveEventsAsync(aggregateRoot.Id, aggregateType, aggregateRoot.UncommittedEvents, version); 
        }


        private string GetStreamName(Guid aggregateId) => $"{_StreamBase}_{aggregateId}"; // Not using it for now.

    }
}
