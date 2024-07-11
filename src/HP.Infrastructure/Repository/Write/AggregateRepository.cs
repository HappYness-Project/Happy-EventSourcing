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
        private readonly string _streamAggergateType;
        public AggregateRepository(IEventStore eventStore)
        {
            _streamAggergateType = typeof(T).Name;
            _eventStore = eventStore;
        }

        public async Task<T> RehydrateAsync<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot, new()
        {
            T aggregate = new T();
            var aggregateType = typeof(T).Name;
            var events = await _eventStore.GetEventsAsync(GetStreamName(id));
            if (events == null || !events.Any()) return aggregate;
            foreach (var eve in events)
                aggregate.When(eve);

            aggregate.Version = events.Select(x => x.AggregateVersion).Max();
            return aggregate;
        }

        public async Task PersistAsync(T aggregateRoot, CancellationToken ct = default)
        {
            if(aggregateRoot == null)
                throw new ArgumentNullException(nameof(aggregateRoot));

            if (!aggregateRoot.UncommittedEvents.Any())
                return;

            var version = aggregateRoot.UncommittedEvents.First().AggregateVersion - 1;
            await _eventStore.SaveEventsAsync(GetStreamName(aggregateRoot.Id), aggregateRoot.UncommittedEvents, version);

            aggregateRoot.ClearEvents();
        }


        private string GetStreamName(Guid aggregateId) => $"{_streamAggergateType}-{aggregateId}"; 

    }
}
