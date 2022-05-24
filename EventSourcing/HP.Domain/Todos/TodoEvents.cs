using HP.Domain.Common;
namespace HP.Domain.Todos
{
    public static class TodoDomainEvents
    {
        public record TodoCreatedEvent : DomainEventBase
        {
            public TodoCreatedEvent(string todoId, string userId, string todoTitle, string type)
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
            public TodoRemovedEvent(string todoId)
            {
                this.TodoId = todoId;   
            }
            public string TodoId { get; }
        }

        public record TodoStatusUpdatedEvent : DomainEventBase
        {
            public TodoStatusUpdatedEvent()
            {
            }

        }

        public record TodoActivatedEvent : DomainEventBase
        {
            public TodoActivatedEvent(string todoId)
            {
                this.TodoId = todoId;
            }
            public string TodoId { get; }
        }

        public record TodoCompletedEvent : DomainEventBase
        {
            public TodoCompletedEvent(string todoId, TodoStatus todoStatus)
            {
                this.TodoId = todoId;
                this.todoStatus = todoStatus;
            }
            public string TodoId { get; }
            public TodoStatus todoStatus { get; }
        }

    }

}
