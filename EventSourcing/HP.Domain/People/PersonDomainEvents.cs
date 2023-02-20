using HP.Core.Models;
namespace HP.Domain
{
    public static class PersonDomainEvents
    {
        public class PersonCreated : DomainEvent
        {
            public Guid PersonId { get; set; }
            public string PersonName { get; set; }
            public string PersonType { get; set; }
            public string PersonRole { get; set; }
        }
        public class PersonInfoUpdated : DomainEvent
        {
            public Guid PersonId { get; set; }
            public string PersonType { get; set; }
            public string PersonRole { get; set; }
            public string GoalType { get; set; }
        }
        public class PersonRoleUpdated : DomainEvent
        {
            public Guid PersonId { get; set; }
            public string PreRole { get; set; }
            public string Role { get; set; }
        }
        public class AddressChanged : DomainEvent
        {
            public Guid PersonId { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string Stress { get; set; }
            public string ZipCode { get; set; }
        }
        public class PersonRoleSetAdminAssigned : DomainEvent
        {
            public Guid PersonId { get; set; }
        }
        public class PersonGroupUpdated : DomainEvent
        {
            public Guid PersonId { get; set; }
            public int GroupId { get; set; }
        }
        public class PersonRemoved : DomainEvent
        {
            public Guid PersonId { get; set;}
        }
    }
}
