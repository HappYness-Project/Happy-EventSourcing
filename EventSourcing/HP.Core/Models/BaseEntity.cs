namespace HP.Core.Models
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        public BaseEntity() { }
        public Guid Id { get; protected set; } = default!;

    }
}
