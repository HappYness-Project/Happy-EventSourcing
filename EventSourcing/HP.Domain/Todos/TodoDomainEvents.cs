using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public static class TodoDomainEvents
    {
        public class TodoCreatedEvent : DomainEventBase
        {
            public TodoCreatedEvent(string todoId, string userId, string todoTitle, string description, string type) 
                : base(entityId: todoId, entityType:nameof(Todo))
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
        public class TodoUpdatedEvent : DomainEventBase
        {
            public TodoUpdatedEvent(string userId, string id, string todoTitle, string description, string type) : base(entityId: id, entityType: nameof(Todo))
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
