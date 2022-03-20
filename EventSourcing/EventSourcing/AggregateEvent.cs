using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public abstract class AggregateEvent<TKey> : IAggregateEvent<TKey>
    {
        protected AggregateEvent(TKey aggregateId, int aggregateVersion, DateTime timeStamp)
        {
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
            TimeStamp = timeStamp;
        }

        public TKey AggregateId { get; }
        public int AggregateVersion { get; }
        public DateTime TimeStamp { get; }
    }
}
