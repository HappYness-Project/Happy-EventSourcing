using HP.Domain.Common;

namespace HP.Domain
{
    public static class PersonEvents
    {
        public class PersonCreated : DomainEventBase
        {
            public PersonCreated(string personId, string firstName, string lastName, Email email, Address address) : base(nameof(Person))
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                Address = address;
            }
            public string FirstName { get; }
            public string LastName { get; }
            public Address Address { get; }
            public Email Email { get; }

        }
        public class PersonUpdated : DomainEventBase
        {
            public PersonUpdated(string personId) : base(nameof(Person))
            {}
        }
        public class PersonRoleUpdated : DomainEventBase
        {
            public PersonRoleUpdated(string personId, string preRole, string curRole) : base(nameof(Person))
            { }

           public string PreRole { get; set; }
            public string Role { get; set; }
        }

        public class AddressChanged : DomainEventBase
        {
            public AddressChanged(string personId, string country, string city, string stress, string zipCode) : base(entityType: nameof(Address))
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
            public PersonRoleSetAdminAssigned(string personId) : base(entityType: nameof(Person))
            {
                Id = personId;
            }
            public string Id { get; }
        }
        public class PersonGroupUpdated : DomainEventBase
        {
            public PersonGroupUpdated(string personId, int groupId) : base(entityType: nameof(Person))
            {
                this.Id = personId;
                this.GroupId = groupId;
            }
            public string Id {get; }
            public int GroupId {get;}
        }
    }
}
