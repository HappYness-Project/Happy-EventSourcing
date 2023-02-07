using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;

namespace HP.Infrastructure.Repository.Write
{
    public class AggregateRepository<T> : IAggregateRepository<T> where T : AggregateRoot
    {
        private readonly IEventStoreRepository _esRepository;
        private readonly string _StreamBase;

        public AggregateRepository(IEventStoreRepository eventStoreRepository)
        {
            _esRepository = eventStoreRepository;
            var aggregateType = typeof(T);
            _StreamBase = aggregateType.Name;
        }
        public async Task PersistAsync(T aggregateRoot, CancellationToken ct = default)
        {
            if(aggregateRoot == null)
                throw new ArgumentNullException(nameof(aggregateRoot));

            if (!aggregateRoot.UncommittedEvents.Any())
                return;

            var streamName = GetStreamName(aggregateRoot.Id);
        }

        public Task<T> RehydrateAsync(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
        private string GetStreamName(Guid aggregateId) => $"{_StreamBase}_{aggregateId}";

    }
}
