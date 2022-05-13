using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public class Todo : Entity, IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}