using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public class Todo : Entity, IAggregateRoot
    {
        public Todo()
        {
            Tag = Array.Empty<string>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string[] Tag { get; set; }
        public bool IsStarted { get; set; }
        public bool IsActive { get; set; }
        public double Score { get; set; }
        public IEnumerable<Todo> SubTodos { get; set; } = Enumerable.Empty<Todo>();
        public TodoStatus Status { get; set; }  
        public DateTime Updated { get; set; }
        public DateTime Completed { get; set; }
    }
}