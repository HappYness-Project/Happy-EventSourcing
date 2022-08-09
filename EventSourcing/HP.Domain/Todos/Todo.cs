using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public class Todo : Entity, IAggregateRoot
    {
        public Todo()
        {
            IsActive = true;
            Tag = Array.Empty<string>();
        }
        public Todo(string userId, string title, string description, string type, string[] tag) : this()
        {
            // TODO : CheckPolicies
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            
            UserId = userId;
            Title = title;
            Description = description;
            Type = type;
            Tag = tag;

            AddDomainEvent(new TodoDomainEvents.TodoCreated(Id, userId, title, Description, type));
        }
        public string UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ProjectId { get; private set; }
        public string Type { get; private set; }
        public string[] Tag { get; private set; }
        public bool IsStarted { get; private set; }
        public bool IsActive { get; private set; }
        public double Score { get; private set; }
        public IEnumerable<Todo> SubTodos { get; private set; } = Enumerable.Empty<Todo>();
        public TodoStatus Status { get; private set; }  
        public string StatusDesc { get; private set; }
        public DateTime? Updated { get; private set; } 
        public DateTime? Completed { get; private set; }


        public static Todo Create(string userId, string title, string description, string type, string[] tags)
        {
            return new Todo(userId, title, description, type, tags);
        }
        // ??? TODO I am not sure i am adding todoItems, or Just Todo from this method.
        public void AddTodoItem(string todoId, string userId, string title, string type)
        {
            // I am not sure if it's just adding the todoItem Extra inside of Todo Object.
            //this.AddDomainEvent(new TodoDomainEvents.TodoCreatedEvent(todoId, userId, title, type));
        }

        public void DeleteTodoItems(string todoId)
        {
            var todo = SubTodos.FirstOrDefault(x => x.Id == todoId);
            if (todo == null)
                throw new Exception("Not Found Todo!!");
            // Not sure!!
            //todo.Delete
        }
        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case TodoDomainEvents.TodoCreated c:
                    break;

                case TodoDomainEvents.TodoUpdated u:
                    break;

                case TodoDomainEvents.TodoActivated a:
                    this.IsActive = true;
                    
                    break;

                case TodoDomainEvents.TodoDeactivated d:
                    break;
            }

        }


        public void ActivateTodo(string todoId)
        {
            this.IsActive = true;
            this.AddDomainEvent(new TodoDomainEvents.TodoActivated(todoId));
        }
        public void DeactivateTodo(string todoId)
        {
            this.IsActive = false;
            this.AddDomainEvent(new TodoDomainEvents.TodoDeactivated(todoId));

        }
        public void SetStatus(string todoId, TodoStatus status)
        {
            switch(status)
            {
                case TodoStatus.Completed:
                    this.Status = status;
                    this.StatusDesc = $"Todo Id:{todoId} of Title: {Title} is completed.";
                    AddDomainEvent(new TodoDomainEvents.TodoCompleted(todoId));
                    break;

                case TodoStatus.Pending:
                    this.Status = status;
                    this.StatusDesc = $"Todo Id:{todoId} of Title: {Title} is pending.";
                    AddDomainEvent(new TodoDomainEvents.TodoStatusToPending(todoId));
                    break;

                default:

                    break;
            }
        }
    }
}