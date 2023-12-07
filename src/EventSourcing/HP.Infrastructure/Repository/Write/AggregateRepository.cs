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

        public Task<List<Guid>> GetAggregateIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByAggregateId<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot, new()
        {
            T aggregate = new T();
            var events = await _eventStore.GetEventsAsync(id);
            if (events == null || !events.Any()) return aggregate;
            foreach (var eve in events)
                aggregate.When(eve);

            aggregate.Version = events.Select(x => x.AggregateVersion).Max();
            return (T)aggregate;
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
