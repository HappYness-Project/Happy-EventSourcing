using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;

namespace HP.Infrastructure.Repository.Write
{
    public class AggregateRepository<T> : IAggregateRepository<T> where T : AggregateRoot
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

        public Task<T> RehydrateAsync(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
        private string GetStreamName(Guid aggregateId) => $"{_StreamBase}_{aggregateId}"; // Not using it for now.

    }
}
