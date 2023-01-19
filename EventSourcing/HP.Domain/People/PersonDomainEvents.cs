using HP.Core.Models;
namespace HP.Domain
{
    public static class PersonDomainEvents
    {
        public class PersonCreated : DomainEvent
        {
            public PersonCreated(Guid id, string personName) : base(id) { PersonId = id; personName = personName; }
            public Guid PersonId { get; set; }
            public string PersonName { get; set; }
        }
        public class PersonInfoUpdated : DomainEvent
        {
            public PersonInfoUpdated(Guid id, string personType, string goalType) : base(id)
            {
                PersonId = id;
                PersonType = personType;
                GoalType = goalType;
            }
            public Guid PersonId { get; set; }
            public string PersonType { get; set; }
            public string GoalType { get; set; }
        }
        public class PersonRoleUpdated : DomainEvent
        {
            public PersonRoleUpdated(Guid id, string preRole, string curRole) : base(id) { PersonId = id; PreRole = preRole; Role = curRole; }
            public Guid PersonId { get; set; }
            public string PreRole { get; set; }
            public string Role { get; set; }
        }
        public class AddressChanged : DomainEvent
        {
            public AddressChanged(Guid id, string country, string city, string stress, string zipCode) : base(id)
            {
                PersonId = id;
                Country = country;
                City = city;
                Stress = stress;
                ZipCode = zipCode;
            }
            public Guid PersonId { get; }
            public string Country { get; }
            public string City { get; }
            public string Stress { get; }
            public string ZipCode { get; }
        }
        public class PersonRoleSetAdminAssigned : DomainEvent
        {
            public PersonRoleSetAdminAssigned(Guid id) : base(id) { PersonId = id; }
            public Guid PersonId { get; }
        }
        public class PersonGroupUpdated : DomainEvent
        {
            public PersonGroupUpdated(Guid id, int groupId) : base(id) { this.PersonId = id; this.GroupId = groupId; }
            public Guid PersonId {get; }
            public int GroupId {get;}
        }
    }
}
