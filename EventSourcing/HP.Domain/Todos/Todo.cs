using HP.Core.Models;
using static HP.Domain.TodoDomainEvents;

namespace HP.Domain
{
    public class Todo : AggregateRoot
    {
        public Todo() { }
        public Todo(Person person, string title, string description, TodoType todoType, string[] tag)
        {
            // TODO : CheckPolicies
            if (person is null)
                throw new ArgumentNullException(nameof(person));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            UserId = person.PersonName;
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
            if (person is null)
                throw new ArgumentNullException(nameof(person));

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
            return todoItem;
        }
        public void DeleteTodoItem(Guid todoItemId)
        {
            var todoItem = SubTodos.FirstOrDefault(x => x.Id == todoItemId);
            if (todoItem == null)
                throw new Exception($"[Domain Excecption]Not Found TodoItem : {todoItemId}");

            SubTodos.Remove(todoItem);
            this.AddDomainEvent(new TodoDomainEvents.TodoItemRemoved(todoItemId));
        }
        public void UpdateTodoItem(Guid todoItemId, string newTitle, string newDesc, string newType)
        {
            var todoItem = SubTodos.FirstOrDefault(x => x.Id == todoItemId);
            if (todoItem == null)
                throw new Exception($"[Domain Excecption]Not Found TodoItem : {todoItemId}");

            todoItem.Update(newTitle, newType, newDesc);
            this.AddDomainEvent(new TodoDomainEvents.TodoItemUpdated(todoItemId));
        }
        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case TodoCreated todoCreated:
                    //this.Title = c.TodoTitle;
                    //this.Type = TodoType.FromName(c.Type);
                    //this.UserId = c.UserId;
                    Apply(todoCreated);
                    break;

                case TodoUpdated todoUpdated:
                    //this.UserId = u.UserId;
                    Apply(todoUpdated);
                    break;

                case TodoActivated todoActivated:
                    //this.IsActive = true;
                    Apply(todoActivated);
                    break;

                case TodoDeactivated todoDeactivated:
                    //this.IsActive = false;
                    Apply(todoDeactivated);
                    break;
            }
        }
        private void Apply(TodoCreated @event) { }
        private void Apply(TodoUpdated @event) { }
        private void Apply(TodoActivated @event) { }
        private void Apply(TodoDeactivated @event) { }
        public void ActivateTodo(Guid todoId)
        {
            this.IsActive = true;
            this.AddDomainEvent(new TodoDomainEvents.TodoActivated(todoId));
        }
        public void DeactivateTodo(Guid todoId)
        {
            this.IsActive = false;
            this.AddDomainEvent(new TodoDomainEvents.TodoDeactivated(todoId));
        }
        public void Remove(Guid todoId)
        {
            this.AddDomainEvent(new TodoDomainEvents.TodoRemoved(todoId));
        }
        public void SetStatus(TodoStatus status, string? reason = null)
        {
            switch (status.Name)
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