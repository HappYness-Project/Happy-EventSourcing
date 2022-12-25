namespace HP.Core.Models
{
    public interface IAggregateRoot : IAggregateRoot<Guid>
    {
        
    }
    public interface IAggregateRoot<out TKey> 
    { 
        TKey Id {get; }
        int Version {get; }
//    object[] DequeueUncommittedEvents();
    }
}
