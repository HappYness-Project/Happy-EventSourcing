using MongoDB.Bson;
namespace HP.Core.Models
{
    public abstract class Entity : BaseEntity
    { 
        public DateTime CreatedDate { get; private set; }
        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreatedDate = DateTime.Now;
        }
        public Entity(string id)
        {
            Id = id;
            CreatedDate = DateTime.Now;
        }
    }
}
