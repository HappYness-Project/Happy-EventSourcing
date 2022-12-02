namespace HP.Core.Models
{
    public abstract class AggregateRoot<T> : IAggregateRoot<T> where T : notnull
    {

    }
}
//An aggregate is a collection of one or more related entities (and possibly value objects). 
// Each Aggregate has a single root entity, referred to as the aggregate root.
// The aggregate root is responsible for controlling access to all of the members of its aggregate.
