using HP.Core.Models;
namespace HP.Domain
{
    public static class PersonDomainEvents
    {
        public class PersonCreated : DomainEvent
        {
            public PersonCreated(Guid personId, string personName)
            {
                PersonId = personId;
                PersonName = personName;
            }
            public Guid PersonId { get; set; }
            public string PersonName { get; set; }
        }
        public class PersonInfoUpdated : DomainEvent
        {
            public PersonInfoUpdated(Guid personId, string personType, string goalType) 
            {
                PersonId = personId;
                PersonType = personType;
                GoalType = goalType;
            }
            public Guid PersonId { get; set; }
            public string PersonType { get; set; }
            public string GoalType { get; set; }
        }
        public class PersonRoleUpdated : DomainEvent
        {
            public PersonRoleUpdated(Guid personId, string preRole, string curRole)
            {
                PersonId = personId;
                PreRole = preRole;
                Role = curRole;
            }
            public Guid PersonId { get; set; }
            public string PreRole { get; set; }
            public string Role { get; set; }
        }

        public class AddressChanged : DomainEvent
        {
            public AddressChanged(string personId, string country, string city, string stress, string zipCode)
            {
                Country = country;
                City = city;
                Stress = stress;
                ZipCode = zipCode;
            }
            public string Country { get; }
            public string City { get; }
            public string Stress { get; }
            public string ZipCode { get; }
        }
        public class PersonRoleSetAdminAssigned : DomainEvent
        {
            public PersonRoleSetAdminAssigned(string personId) 
            {
                PersonId = personId;
            }
            public string PersonId { get; }
        }
        public class PersonGroupUpdated : DomainEvent
        {
            public PersonGroupUpdated(Guid personId, int groupId)
            {
                this.PersonId = personId;
                this.GroupId = groupId;
            }
            public Guid PersonId {get; }
            public int GroupId {get;}
        }
    }
}
