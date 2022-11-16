using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain
{
    public interface IAggregateRoot<T> { }

    public abstract class Aggregate<T>: IAggregateRoot<T> where T : notnull
    {

    }
}



//An aggregate is a collection of one or more related entities (and possibly value objects). 
// Each Aggregate has a single root entity, referred to as the aggregate root.
// The aggregate root is responsible for controlling access to all of the members of its aggregate.
// it's perfectly acceptable f