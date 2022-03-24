using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class AggregateRoot<TId> : IAggregateRoot<TId>
    {
        public TId Id { get; protected set; }
        public long Version { get; internal set; }

        public IReadOnlyCollection<IDomainEvent<TId>> Events => throw new NotImplementedException();


        public void ClearEvents()
        {
            throw new NotImplementedException();
        }
    }
}
