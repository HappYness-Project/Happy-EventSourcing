using HP.Core.Common;
using HP.Core.Models;

namespace HP.Core.Test.Dummy
{
    public class DummyAggregateRepository<T> : IAggregateRepository<T> where T : IAggregateRoot
    {
        public List<StreamAction> AggregateStream = new();
        public async Task PersistAsync(T aggregate, CancellationToken ct = default)
         =>   AggregateStream.Add(new StreamAction(aggregate.Id,aggregate, aggregate.Version + 1, aggregate.UncommittedEvents));

        public Task<T> RehydrateAsync<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot, new()
        {
            var check  = AggregateStream.FirstOrDefault(x => x.Stream == id)!.Aggregate;
            return Task.FromResult<T>(check as T);
        }
        public record class StreamAction(Guid Stream, T Aggregate, long ExpectedVersion, IEnumerable<object> Events);
    }
}
