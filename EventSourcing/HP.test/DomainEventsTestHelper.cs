using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HP.test
{
    public class DomainEventsTestHelper
    {
        public static List<IDomainEvent> GetAllDomainEvents(Entity aggregate)
        {
            List<IDomainEvent> domainEvents = new List<IDomainEvent>();
            if(aggregate.DomainEvents != null)
            {
                domainEvents.AddRange(aggregate.DomainEvents);
            }
            var fields = aggregate.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Concat(aggregate.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)).ToArray();

            return null;
        }
    }
}
