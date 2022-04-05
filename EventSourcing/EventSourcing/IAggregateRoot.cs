using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
    public interface IAggregateRoot<out TKey> : IEntity<TKey>
    {
        public long Version { get; }
        IReadOnlyCollection<IDomainEvent<TKey>> Events { get; }

        void ClearEvents();

    }
}
