using HP.Core.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HP.Domain.Test
{
    public class DomainEventsTestHelper
    {
        public static List<IDomainEvent> GetAllDomainEvents(AggregateRoot aggregate)
        {
            List<IDomainEvent> domainEvents = new List<IDomainEvent>();
            if(aggregate.UncommittedEvents != null)
            {
                domainEvents.AddRange(aggregate.UncommittedEvents);
            }
            var fields = aggregate.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Concat(aggregate.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)).ToArray();
            foreach(var field in fields)
            {
                var isAggregate = field.FieldType.IsAssignableFrom(typeof(AggregateRoot));
                if (isAggregate)
                {
                    var aggregateRoot = field.GetValue(aggregate) as AggregateRoot;
                    domainEvents.AddRange(GetAllDomainEvents(aggregateRoot).ToList());
                }
                if(field.FieldType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                {
                    if(field.GetValue(aggregate) is IEnumerable enumerable)
                    {
                        foreach(var en in enumerable)
                        {
                            if(en is AggregateRoot entityItem)
                            {
                                domainEvents.AddRange(GetAllDomainEvents(entityItem));
                            }
                        }
                    }
                }
            }
            return domainEvents;
        }
    }
}
