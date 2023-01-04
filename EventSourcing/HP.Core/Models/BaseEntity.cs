namespace HP.Core.Models
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        public BaseEntity() {
            this.Created = DateTime.Now;
        }
        public Guid Id { get; protected set; } = default!;
        public DateTime? Created { get; }
    }
}
