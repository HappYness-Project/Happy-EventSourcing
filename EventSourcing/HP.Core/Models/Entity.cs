using MongoDB.Bson;
namespace HP.Core.Models
{
    public abstract class Entity : BaseEntity
    { 
        public DateTime CreatedDate { get; private set; }
        public Entity() { }
        public Entity(Guid id)
        {
            Id = id;
            CreatedDate = DateTime.Now;
        }
    }
}
