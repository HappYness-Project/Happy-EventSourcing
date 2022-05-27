using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public static class TodoDomainEvents
    {
        public record TodoCreatedEvent : DomainEventBase
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
        public record TodoRemovedEvent : DomainEventBase
        {
            public TodoRemovedEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;   
            }
            public string TodoId { get; }
        }
        public record TodoStatusUpdatedEvent : DomainEventBase
        {
            public TodoStatusUpdatedEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }

        }
        public record TodoStatusToPendingEvent : DomainEventBase 
        {
            public TodoStatusToPendingEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }
        public record TodoActivatedEvent : DomainEventBase
        {
            public TodoActivatedEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }
        public record TodoCompletedEvent : DomainEventBase
        {
            public TodoCompletedEvent(string todoId) : base(entityId: todoId, entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        } 
    }

}
