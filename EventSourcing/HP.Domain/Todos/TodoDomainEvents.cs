using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public static class TodoDomainEvents
    {
        public class TodoCreatedEvent : DomainEventBase
        {
            public TodoCreatedEvent(string todoId, string userId, string todoTitle, string type) 
                : base(entityId: todoId, entityType:nameof(Todo))
            {
                this.TodoId = todoId;
                this.UserId = userId;
                this.TodoTitle = todoTitle;
                this.Type = type;
            }
            public string TodoId { get; }
            public string UserId { get; }
            public string TodoTitle { get; }
            public string Type { get; }
        }
        public class TodoRemovedEvent : DomainEventBase
        {
            public TodoRemovedEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;   
            }
            public string TodoId { get; }
        }
        public class TodoStatusToPendingEvent : DomainEventBase 
        {
            public TodoStatusToPendingEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }
        public class TodoActivatedEvent : DomainEventBase
        {
            public TodoActivatedEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }
        public class TodoDeactivatedEvent : DomainEventBase
        {
            public TodoDeactivatedEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }
        public class TodoCompletedEvent : DomainEventBase
        {
            public TodoCompletedEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        } 
    }

}
