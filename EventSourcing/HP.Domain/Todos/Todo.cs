using HP.Domain.Common;
namespace HP.Domain
{
    public class Todo : Entity
    {
        protected Todo(string id) : base(id)
        {
            IsActive = true;
            Tag = Array.Empty<string>();
        }
        public Todo(Person person, string title, string description, TodoType todoType, string[] tag) 
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
            IsDone = false;
            SubTodos = new HashSet<TodoItem>();
            AddDomainEvent(new TodoDomainEvents.TodoCreated(Id, UserId, title, todoType.Name));
        }
        public string UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ProjectId { get; private set; }
        public TodoType Type { get; private set; }
        public string[] Tag { get; private set; }
        public bool IsDone { get; private set; }
        public bool IsActive { get; private set; }
        public double Score { get; private set; }
        public ICollection<TodoItem> SubTodos { get; private set; }
        public TodoStatus Status { get; private set; }  
        public string StatusDesc { get; private set; }
        public DateTime? TargetStartDate { get; private set; }
        public DateTime? TargetEndDate { get; private set; }
        public DateTime? Updated { get; private set; }  
        public DateTime? Completed { get; private set; }
        public static Todo Create(Person person, string title, string description, TodoType type, string[] tags)
        {
            if(person is null)
                throw new ArgumentNullException(nameof(person));

            // Get Todo Type
            return new(person, title, description, type, tags);
        }
        public void Update(string title, string type, string desc, string[] Tags, DateTime? targetStartDate = null, DateTime? targetEndDate = null)
        {
            this.Title = title;
            this.Description = desc;
            this.Type = TodoType.FromName(type);
            this.TargetStartDate = targetStartDate ?? null;
            this.TargetEndDate = targetEndDate ?? null;
            this.Tag = Tags;
            this.Updated = DateTime.Now;
            this.AddDomainEvent(new TodoDomainEvents.TodoUpdated(UserId, Id, Title, Type.Name));
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
        public void UpdateTodoItem(string todoItemId, string newTitle, string newDesc, string newType)
        {
            var todoItem = SubTodos.FirstOrDefault(x => x.Id == todoItemId);
            if (todoItem == null)
                throw new Exception($"[Domain Excecption]Not Found TodoItem : {todoItemId}");

            todoItem.Title = newTitle;
            todoItem.Description = newDesc;
            todoItem.TodoType = newType;
            this.AddDomainEvent(new TodoDomainEvents.TodoItemUpdated(todoItemId));
        }
        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case TodoDomainEvents.TodoCreated c:
                    this.Title = c.TodoTitle;
                    this.Type = TodoType.FromName(c.Type);
                    this.UserId = c.UserId;
                    break;

                case TodoDomainEvents.TodoUpdated u:
                    this.UserId = u.UserId;
                    break;

                case TodoDomainEvents.TodoActivated a:
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
            switch(status.Name)
            {
                case "pending":
                    this.Status = TodoStatus.Pending;
                    this.StatusDesc = $"Todo Id:{Id} of Title: {Title} is completed.";
                    AddDomainEvent(new TodoDomainEvents.TodoStatusToPending(Id));
                    break;

                case "accept":
                    this.Status = TodoStatus.Accept;
                    this.StatusDesc = $"Todo Id:{Id} of Title: {Title} is accepted.";
                    AddDomainEvent(new TodoDomainEvents.TodoStatusToAccepted(Id));
                    break;

                case "start":
                    this.Status = TodoStatus.Start;
                    this.StatusDesc = $"Todo Id:{Id}, has been started at {DateTime.Now}";
                    AddDomainEvent(new TodoDomainEvents.TodoStarted(Id));
                    break;

                case "complete":
                    this.Status = TodoStatus.Complete;
                    this.StatusDesc = $"Todo Id:{Id} is completed. ";
                    this.IsDone = true;
                    this.Completed = DateTime.Now;
                    AddDomainEvent(new TodoDomainEvents.TodoCompleted(Id));
                    break;

                case "stop":
                    this.Status = TodoStatus.Stop;
                    this.StatusDesc = $"Todo Id:{Id}, has been stopped. Reason: {reason}";
                    //AddDomainEvent(new TodoDomainEvents.TodoStopped);
                    break;
                default:
                    break;
            }
        }
    }
}