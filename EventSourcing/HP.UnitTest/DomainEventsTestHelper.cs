using HP.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HP.test
{
    public class DomainEventsTestHelper
    {
        public static List<IDomainEvent> GetAllDomainEvents(AggregateRoot<string> aggregate)
        {
            List<IDomainEvent> domainEvents = new List<IDomainEvent>();
            if(aggregate.GetUncommittedChanges != null)
            {
                domainEvents.AddRange(aggregate.GetUncommittedChanges());
            }
            var fields = aggregate.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Concat(aggregate.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)).ToArray();
            return null;
        }
    }
}
