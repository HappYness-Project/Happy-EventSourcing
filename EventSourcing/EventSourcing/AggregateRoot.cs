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
        public long Version { get; protected set; }
        public IReadOnlyCollection<IDomainEvent> DomainEvents { get; set; } = new List<IDomainEvent>();
        public void ClearEvents()
        {
           // DomainEvents?.Clear();
        }
    }
}
