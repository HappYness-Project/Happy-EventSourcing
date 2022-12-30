using HP.Core.Models;
namespace HP.Domain
{
    public static class PersonDomainEvents
    {
        public class PersonCreated : DomainEventBase
        {
            public PersonCreated(string personId)
            {
            }
        }
        public class PersonUpdated : DomainEventBase
        {
            public PersonUpdated(string personId) 
            {}
        }
        public class PersonRoleUpdated : DomainEventBase
        {
            public PersonRoleUpdated(Guid personId, string preRole, string curRole)
            { }

           public string PreRole { get; set; }
            public string Role { get; set; }
        }

        public class AddressChanged : DomainEventBase
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
        public class PersonRoleSetAdminAssigned : DomainEventBase
        {
            public PersonRoleSetAdminAssigned(string personId) 
            {
                Id = personId;
            }
            public string Id { get; }
        }
        public class PersonGroupUpdated : DomainEventBase
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
