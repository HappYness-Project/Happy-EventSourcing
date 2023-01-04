using HP.Core.Models;
namespace HP.Domain
{
    public static class PersonDomainEvents
    {
        public class PersonCreated : DomainEvent
        {
            public PersonCreated(string personId, string personName)
            {
                PersonName = personName;
            }
            public string PersonName { get; set; }
        }
        public class PersonInfoUpdated : DomainEvent
        {
            public PersonInfoUpdated(Guid personId) 
            {
                this.PersonId = personId;   
            }
            public Guid PersonId { get; set; }
        }
        public class PersonRoleUpdated : DomainEvent
        {
            public PersonRoleUpdated(Guid personId, string preRole, string curRole)
            { 

                this.PreRole = preRole;
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
                Id = personId;
            }
            public string Id { get; }
        }
        public class PersonGroupUpdated : DomainEvent
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
