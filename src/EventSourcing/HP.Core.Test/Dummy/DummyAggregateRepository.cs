using HP.Core.Common;
using HP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Core.Test.Dummy
{
    public class DummyAggregateRepository<T> : IAggregateRepository<T> where T : IAggregateRoot
    {
        public List<StreamAction> AggregateStream = new();
        public async Task PersistAsync(T aggregate, CancellationToken ct = default)
         =>   AggregateStream.Add(new StreamAction(aggregate.Id,aggregate, aggregate.Version + 1, aggregate.UncommittedEvents));

        public Task<T> RehydrateAsync<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot, new()
        {
            var check = Task.FromResult(AggregateStream.FirstOrDefault(x => x.Stream == id)!.Aggregate).ConfigureAwait(false);
            return check;
        }
        public record class StreamAction(Guid Stream, T Aggregate, long ExpectedVersion, IEnumerable<object> Events);
    }
}
