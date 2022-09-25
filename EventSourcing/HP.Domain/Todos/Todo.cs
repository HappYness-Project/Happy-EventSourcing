using HP.Domain.Common;
namespace HP.Domain
{
    public class Todo : Entity, IAggregateRoot
    {
        protected Todo()
        {
            IsActive = true;
            Tag = Array.Empty<string>();
        }
        public Todo(Person person, string title, string description, string type, string[] tag) : this()
        {
            // TODO : CheckPolicies
            if (person is null)
                throw new ArgumentNullException(nameof(person));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            
            UserId = person.UserId;
            Title = title;
            Description = description;
            Type = type;
            Tag = tag;
            IsActive = true;
            SubTodos = new HashSet<TodoItem>();
            AddDomainEvent(new TodoDomainEvents.TodoCreated(Id, UserId, title, type));
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
        public ICollection<TodoItem> SubTodos { get; private set; }
        public TodoStatus Status { get; private set; }  
        public string StatusDesc { get; private set; }
        public DateTime? Updated { get; private set; } 
        public DateTime? Completed { get; private set; }
        public static Todo Create(Person person, string title, string description, string type, string[] tags)
        {
            if(person is null)
                throw new ArgumentNullException(nameof(person));

            return new(person, title, description, type, tags);
        }
        public TodoItem AddTodoItem(string title, string type, string desc)
        {
            TodoItem todoItem = new TodoItem(title, type, desc);
            var sub = SubTodos.Append(todoItem);
            //this.AddDomainEvent(new TodoDomainEvents.TodoCreatedEvent(todoId, userId, title, type));
            return new TodoItem(title, type, desc);
        }
        public void DeleteTodoItem(string todoItemId)
        {
            var todoItem = SubTodos.FirstOrDefault(x => x.Id == todoItemId);
            if (todoItem == null)
                throw new Exception($"[Domain Excecption]Not Found TodoItem : {todoItemId}");

            SubTodos.Remove(todoItem);
            this.AddDomainEvent(new TodoDomainEvents.TodoItemRemoved(todoItemId));
        }
        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case TodoDomainEvents.TodoCreated c:
                    this.Id = c.AggregateId.ToString();
                    this.Title = c.TodoTitle;
                    this.Type = c.Type;
                    this.UserId = c.UserId;
                    break;

                case TodoDomainEvents.TodoUpdated u:
                    this.Id = u.AggregateId.ToString();
                    this.UserId = u.UserId;
                    break;

                case TodoDomainEvents.TodoActivated a:
                    this.Id = a.TodoId;
                    this.IsActive = true;
                    break;

                case TodoDomainEvents.TodoDeactivated d:
                    this.IsActive = false;
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
        public void Remove(string todoId)
        {
            this.AddDomainEvent(new TodoDomainEvents.TodoRemoved(todoId));
        }
        public void SetStatus(string todoId, TodoStatus status, string? reason =null)
        {
            switch(status.ToString())
            {
                case "Pending":
                    this.Status = TodoStatus.Pending;
                    this.StatusDesc = $"Todo Id:{todoId} of Title: {Title} is completed.";
                    AddDomainEvent(new TodoDomainEvents.TodoCompleted(todoId));
                    break;

                case "Accepted":
                    this.Status = TodoStatus.Accepted;
                    this.StatusDesc = $"Todo Id:{todoId} of Title: {Title} is accepted.";
                    AddDomainEvent(new TodoDomainEvents.TodoStatusToAccepted(todoId));
                    break;

                case "Started":
                    this.Status = TodoStatus.Started;
                    this.IsStarted = true;
                    this.StatusDesc = $"Todo Id:{todoId}, has been started at {DateTime.Now}";
                    AddDomainEvent(new TodoDomainEvents.TodoStarted(todoId));
                    break;

                case "Completed":
                    this.Status = TodoStatus.Completed;
                    this.StatusDesc = $"Todo Id:{todoId} is completed. ";
                    AddDomainEvent(new TodoDomainEvents.TodoCompleted(todoId));
                    break;

                case "Stopped":
                    this.Status = TodoStatus.Stopped;
                    this.StatusDesc = $"Todo Id:{todoId}, has been stopped. Reason: {reason}";
                    break;
                default:
                    break;
            }
        }
    }
}