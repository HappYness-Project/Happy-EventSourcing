using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public static class TodoDomainEvents
    {
        public class TodoCreated : DomainEventBase
        {
            public TodoCreated(string todoId, string userId, string todoTitle, string description, string type) 
                : base(entityType:nameof(Todo))
            {
                this.TodoId = todoId;
                this.UserId = userId;
                this.TodoTitle = todoTitle;
                this.Description = description;
                this.Type = type;
            }
            public string TodoId { get; }
            public string UserId { get; }
            public string TodoTitle { get; }
            public string Description { get; }
            public string Type { get; }
        }
        public class TodoUpdated : DomainEventBase
        {
            public TodoUpdated(string userId, string id, string todoTitle, string description, string type) : base(entityType: nameof(Todo))
            {
                UserId = userId;
                TodoId = id;
                TodoTitle = todoTitle;
                Description = description;
                TodoType = type;
            }
            public string UserId { get; set; }
            public string TodoId { get; set; }
            public string TodoTitle { get; set; }
            public string Description { get; set; }
            public string TodoType { get; set; }
        }
        public class TodoRemoved : DomainEventBase
        {
            public TodoRemoved(string todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;   
            }
            public string TodoId { get; }
        }
        public class TodoStatusToPending : DomainEventBase 
        {
            public TodoStatusToPending(string todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }
        public class TodoActivated : DomainEventBase
        {
            public TodoActivated(string todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }
        public class TodoDeactivated : DomainEventBase
        {
            public TodoDeactivated(string todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }
        public class TodoCompleted : DomainEventBase
        {
            public TodoCompleted(string todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        } 
    }
}
