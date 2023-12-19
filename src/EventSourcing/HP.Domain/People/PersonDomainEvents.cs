using HP.Core.Models;
namespace HP.Domain
{
    public static class PersonDomainEvents
    {
        public class PersonCreated : DomainEvent
        {
            public string Email { get; set; }
            public string DisplayName { get; set; }
            public string PersonType { get; set; }
            public string PersonRole { get; set; }
        }
        public class PersonInfoUpdated : DomainEvent
        {
            public string PersonType { get; set; }
            public string GoalType { get; set; }
        }
        public class PersonRoleUpdated : DomainEvent
        {
            public string PreRole { get; set; }
            public string Role { get; set; }
        }
        public class PersonGroupUpdated : DomainEvent
        {
            public int GroupId { get; set; }
        }
        public class PersonRemoved : DomainEvent { }
    }
}
