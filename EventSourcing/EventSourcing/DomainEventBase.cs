using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            OccuredOn = DateTime.Now;
        }
        public DateTime OccuredOn { get; }
    }
}
//https://github.com/kgrzybek/sample-dotnet-core-cqrs-api

