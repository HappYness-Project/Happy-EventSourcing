using HP.Core.Models;
namespace HP.Domain
{
    public static class TodoDomainEvents
    {
        public class TodoCreated : DomainEvent
        {
            public TodoCreated(Guid id, string PersonId, string todoTitle, string todoDesc, string todoType) : base(id)
            {
                this.TodoId = id;
                this.PersonId = PersonId;
                this.TodoTitle = todoTitle;
                this.TodoDesc = todoDesc;
                this.TodoType = todoType;
            }
            public Guid TodoId { get; }
            public string PersonId { get; }
            public string TodoTitle { get; }
            public string TodoDesc { get; }
            public string TodoType { get; }
        }
        public class TodoUpdated : DomainEvent
        {
            public TodoUpdated(Guid id, string todoTitle, string todoDesc, string type) : base(id)
            {
                TodoId = id;
                TodoTitle = todoTitle;
                TodoDesc = todoDesc;
                TodoType = type;
            }
            public Guid TodoId { get; }
            public string TodoTitle { get; }
            public string TodoDesc { get; }
            public string TodoType { get; }
        }
        public class TodoRemoved : DomainEvent
        {
            public TodoRemoved(Guid id) : base(id) { this.TodoId = id; }
            public Guid TodoId { get; }
        }
        public class TodoStatusToPending : DomainEvent
        {
            public TodoStatusToPending(Guid id) : base(id) { this.TodoId = id; }
            public Guid TodoId { get; }
        }
        public class TodoStatusToAccepted : DomainEvent
        {
            public TodoStatusToAccepted(Guid id) : base(id) { this.TodoId = id; }
            public Guid TodoId { get; }
        }
        public class TodoActivated : DomainEvent
        {
            public TodoActivated(Guid id) : base(id) { this.TodoId = id; }
            public Guid TodoId { get; }
        }
        public class TodoDeactivated : DomainEvent
        {
            public TodoDeactivated(Guid id) : base(id) { this.TodoId = id; }
            public Guid TodoId { get; }
        }
        public class TodoStarted : DomainEvent
        {
            public TodoStarted(Guid id) : base(id) { this.TodoId = id; }
            public Guid TodoId { get; }
        }
        public class TodoCompleted : DomainEvent
        {
            public TodoCompleted(Guid id) : base(id) { this.TodoId = id; }
            public Guid TodoId { get; }
        }
        public class TodoItemRemoved : DomainEvent
        {
            public TodoItemRemoved(Guid todoItemId) : base(todoItemId)
            {
                if (todoItemId == null)
                    throw new ArgumentNullException(nameof(todoItemId));
                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
        }
        public class TodoItemCreated : DomainEvent
        {
            public TodoItemCreated(Guid todoItemId, Guid todoId, string title, string desc, string type, DateTime? created, DateTime? targetStartDate, DateTime? targetEndDate) : base(todoItemId) 
            {
                TodoItemId = todoItemId;
                TodoId = todoId;
                TodoTitle = title;
                TodoDesc = desc;
                TodoType = type;
            }
            public Guid TodoItemId { get; }
            public Guid TodoId { get; }
            public string TodoTitle { get; }
            public string TodoDesc { get; }
            public string TodoType { get; }
            public DateTime Created { get; }
            public DateTime TargetStartDate { get; }
            public DateTime TargetEndDate { get; }
        }
        public class TodoItemUpdated : DomainEvent
        {
            public TodoItemUpdated(Guid todoItemId) : base(todoItemId)
            {
                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
            public string TodoTitle { get; }
            public string TodoDesc { get; }
            public string TodoType { get; }
            public DateTime Created { get; }
            public DateTime TargetStartDate { get; }
            public DateTime TargetEndDate { get; }
        }
    }
}
