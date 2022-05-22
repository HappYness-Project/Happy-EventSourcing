using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public class Todo : Entity, IAggregateRoot
    {
        public Todo()
        {
            Tag = Array.Empty<string>();
        }
        public Todo(string userId, string title, string type, string[] tag, IEnumerable<Todo> subTodos) 
            : this()
        {
            AddTodoItem(this.Id, userId, title, type);
        }
        public string UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public string[] Tag { get; private set; }
        public bool IsStarted { get; private set; }
        public bool IsActive { get; private set; }
        public double Score { get; private set; }
        public IEnumerable<Todo> SubTodos { get; private set; } = Enumerable.Empty<Todo>();
        public TodoStatus Status { get; private set; }  
        public DateTime? Updated { get; private set; } 
        public DateTime? Completed { get; private set; }

        public void AddTodoItem(string todoId, string userId, string title, string type)
        {
            this.AddDomainEvent(new TodoCreatedEvent(todoId, userId, title, type));
        }

    }
}