namespace HP.Domain
{
    public interface IAggregateRoot<out TKey>
    {

    }
}



//An aggregate is a collection of one or more related entities (and possibly value objects). 
// Each Aggregate has a single root entity, referred to as the aggregate root.
// The aggregate root is responsible for controlling access to all of the members of its aggregate.
