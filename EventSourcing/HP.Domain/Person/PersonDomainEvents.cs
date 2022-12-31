using HP.Core.Models;
namespace HP.Domain
{
    public static class PersonDomainEvents
    {
        public class PersonCreated : IDomainEvent
        {
            public PersonCreated(string personId, string personName)
            {
                PersonName = personName;
            }
            public string PersonName { get; set; }
        }
        public class PersonUpdated : IDomainEvent
        {
            public PersonUpdated(string personId) 
            {}
        }
        public class PersonRoleUpdated : IDomainEvent
        {
            public PersonRoleUpdated(Guid personId, string preRole, string curRole)
            { }

           public string PreRole { get; set; }
            public string Role { get; set; }
        }

        public class AddressChanged : IDomainEvent
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
        public class PersonRoleSetAdminAssigned : IDomainEvent
        {
            public PersonRoleSetAdminAssigned(string personId) 
            {
                Id = personId;
            }
            public string Id { get; }
        }
        public class PersonGroupUpdated : IDomainEvent
        {
            public PersonGroupUpdated(Guid personId, int groupId)
            {
                this.Id = personId;
                this.GroupId = groupId;
            }
            public Guid Id {get; }
            public int GroupId {get;}
        }
    }
}
