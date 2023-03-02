namespace HP.Core.Models
{
    public interface IEntity : IEntity<Guid> { }
    public interface IEntity<out TKey> 
    { 
        TKey Id { get; }
    }
}
