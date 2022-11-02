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
        public Todo(Person person, string title, string description, TodoType todoType, string[] tag) : this()
        {
            // TODO : CheckPolicies
            if (person is null)
                throw new ArgumentNullException(nameof(person));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            
            UserId = person.UserId;
            Title = title;
            Description = description;
            Type = todoType;
            Tag = tag;
            IsActive = true;
            SubTodos = new HashSet<TodoItem>();
            AddDomainEvent(new TodoDomainEvents.TodoCreated(Id, UserId, title, todoType));
        }
        public string UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ProjectId { get; private set; }
        public TodoType Type { get; private set; }
        public string[] Tag { get; private set; }
        public bool IsStarted { get; private set; }
        public bool IsActive { get; private set; }
        public double Score { get; private set; }
        public ICollection<TodoItem> SubTodos { get; private set; }
        public TodoStatus Status { get; private set; }  
        public string StatusDesc { get; private set; }
        public DateTime? Updated { get; private set; } 
        public DateTime? Completed { get; private set; }
        public static Todo Create(Person person, string title, string description, TodoType type, string[] tags)
        {
            if(person is null)
                throw new ArgumentNullException(nameof(person));

            // Get Todo Type
            return new(person, title, description, type, tags);
        }
        public void Update(string title, string desc, string[] Tags)
        {
            this.Title = title;
            this.Description = desc;
            this.Tag = Tags;
        }
        public TodoItem AddTodoItem(string title, string type, string desc)
        {
            TodoItem todoItem = new TodoItem(title, type, desc);
            SubTodos.Add(todoItem);
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
        public void SetStatus(TodoStatus status, string? reason =null)
        {
            switch(status.Name.ToUpper())
            {
                case "PENDING":
                    this.Status = TodoStatus.Pending;
                    this.StatusDesc = $"Todo Id:{Id} of Title: {Title} is completed.";
                    AddDomainEvent(new TodoDomainEvents.TodoStatusToPending(Id));
                    break;

                case "ACCEPTED":
                    this.Status = TodoStatus.Accepted;
                    this.StatusDesc = $"Todo Id:{Id} of Title: {Title} is accepted.";
                    AddDomainEvent(new TodoDomainEvents.TodoStatusToAccepted(Id));
                    break;

                case "STARTED":
                    this.Status = TodoStatus.Started;
                    this.IsStarted = true;
                    this.StatusDesc = $"Todo Id:{Id}, has been started at {DateTime.Now}";
                    AddDomainEvent(new TodoDomainEvents.TodoStarted(Id));
                    break;

                case "COMPLETED":
                    this.Status = TodoStatus.Completed;
                    this.StatusDesc = $"Todo Id:{Id} is completed. ";
                    AddDomainEvent(new TodoDomainEvents.TodoCompleted(Id));
                    break;

                case "STOPPED":
                    this.Status = TodoStatus.Stopped;
                    this.StatusDesc = $"Todo Id:{Id}, has been stopped. Reason: {reason}";
                    //AddDomainEvent(new TodoDomainEvents.TodoStopped);
                    break;
                default:
                    break;
            }
        }
    }
}