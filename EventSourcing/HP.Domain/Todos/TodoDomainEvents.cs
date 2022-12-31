using HP.Core.Models;
namespace HP.Domain
{
    public static class TodoDomainEvents
    {
        public class TodoCreated : IDomainEvent
        {
            public TodoCreated(Guid todoId, string userId, string todoTitle, string todoType) 
            {
                this.TodoId = todoId;
                this.UserId = userId;
                this.TodoTitle = todoTitle;
                this.Type = todoType;
            }
            public Guid TodoId { get; }
            public string UserId { get; }
            public string TodoTitle { get; }
            public string Type { get; }
        }
        public class TodoUpdated : IDomainEvent
        {
            public TodoUpdated(string userId, Guid id, string todoTitle, string type)
            {
                UserId = userId;
                TodoId = id;
                TodoTitle = todoTitle;
                TodoType = type;
            }
            public string UserId { get; set; }
            public Guid TodoId { get; set; }
            public string TodoTitle { get; set; }
            public string TodoType { get; set; }
        }
        public class TodoRemoved : IDomainEvent
        {
            public TodoRemoved(Guid todoId)
            {
                this.TodoId = todoId;   
            }
            public Guid TodoId { get; }
        }
        public class TodoStatusToPending : IDomainEvent
        {
            public TodoStatusToPending(Guid todoId)
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoStatusToAccepted : IDomainEvent
        {
            public TodoStatusToAccepted(Guid todoId) 
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }


        public class TodoActivated : IDomainEvent
        {
            public TodoActivated(Guid todoId) 
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoDeactivated : IDomainEvent
        {
            public TodoDeactivated(Guid todoId)
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoStarted : IDomainEvent
        {
            public TodoStarted(Guid todoId)
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        } 
        public class TodoCompleted : IDomainEvent
        {
            public TodoCompleted(Guid todoId)
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        } 
        public class TodoItemRemoved : IDomainEvent
        {
            public TodoItemRemoved(Guid todoItemId) 
            {
                if (todoItemId == null)
                    throw new ArgumentNullException(nameof(todoItemId));

                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
        }
        public class TodoItemUpdated : IDomainEvent
        {
            public TodoItemUpdated(Guid todoItemId) 
            {
                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
        }
    }
}
