using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public class Todo : Entity, IAggregateRoot
    {
        public Todo()
        {
            Tag = Array.Empty<string>();
        }
        public Todo(string userId, string title, string type, string[] tag) 
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
        public string StatusDesc { get; private set; }
        public DateTime? Updated { get; private set; } 
        public DateTime? Completed { get; private set; }

        public void AddTodoItem(string todoId, string userId, string title, string type)
        {

            // I am not sure if it's just adding the todoItem Extra inside of Todo Object.
            this.AddDomainEvent(new TodoDomainEvents.TodoCreatedEvent(todoId, userId, title, type));
        }

        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case TodoDomainEvents.TodoCreatedEvent c:
                    this.Id = c.EntityId; // How the Entity Id is used????????
                    break;
            }

            throw new NotImplementedException();
        }


        public void ActivateTodo(string todoId)
        {
            this.IsActive = true;
            this.AddDomainEvent(new TodoDomainEvents.TodoActivatedEvent(todoId));

        }
        public void DeactivateTodo(string todoId)
        {
            this.IsActive = false;
            //this.AddDomainEvent(new TodoDomainEvents.TodoDeActivatedEvent(todoId));

        }
        public void SetStatus(string todoId, TodoStatus status)
        {
            switch(status)
            {
                case TodoStatus.Completed:
                    this.StatusDesc = $"Todo Id:{todoId} of Title: {Title} is completed.";
                    AddDomainEvent(new TodoDomainEvents.TodoCompletedEvent(todoId, status));
                    break;

                case TodoStatus.Pending:
                    this.StatusDesc = $"Todo Id:{todoId} of Title: {Title} is pending.";
              //      AddDomainEvent(new TodoDomainEvents.TodoStatusToPendingEvent(todoId, status));
                    break;

                default:

                    break;
            }
        }
    }
}