using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public class Todo : Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}