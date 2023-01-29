using HP.Core.Models;
namespace HP.Domain
{
    public static class TodoDomainEvents
    {
        public class TodoCreated : DomainEvent
        {
            public Guid TodoId { get; set; }
            public string PersonId { get; set; }
            public string TodoTitle { get; set; }
            public string TodoDesc { get; set; }
            public string TodoType { get; set; }
        }
        public class TodoUpdated : DomainEvent
        {
            public Guid TodoId { get; set; }
            public string TodoTitle { get; set; }
            public string TodoDesc { get; set; }
            public string TodoType { get; set; }
        }
        public class TodoRemoved : DomainEvent
        {
            public Guid TodoId { get; set; }
        }
        public class TodoStatusToPending : DomainEvent
        {
            public Guid TodoId { get; set; }
        }
        public class TodoStatusToAccepted : DomainEvent
        {
            public Guid TodoId { get; set; }
        }
        public class TodoActivated : DomainEvent
        {
            public Guid TodoId { get; set; }
        }
        public class TodoDeactivated : DomainEvent
        {
            public Guid TodoId { get; set; }
        }
        public class TodoStarted : DomainEvent
        {
            public Guid TodoId { get; set; }
        }
        public class TodoCompleted : DomainEvent
        {
            public Guid TodoId { get; set; }
        }
        public class TodoItemRemoved : DomainEvent
        {
            public Guid TodoItemId { get; set; }
        }
        public class TodoItemCreated : DomainEvent
        {
            public Guid TodoItemId { get; set; }
            public Guid TodoId { get; set; }
            public string TodoTitle { get; set; }
            public string TodoDesc { get; set; }
            public string TodoType { get; set; }
            public DateTime? TargetStartDate { get; set; } = null;
            public DateTime? TargetEndDate { get; set; } = null;
        }
        public class TodoItemUpdated : DomainEvent
        {
            public Guid TodoItemId { get; set; }
            public string TodoTitle { get; set; }
            public string TodoDesc { get; set; }
            public string TodoType { get; set; }
            public DateTime Created { get; set; }
            public DateTime TargetStartDate { get; set; }
            public DateTime TargetEndDate { get; set; }
        }
    }

}
