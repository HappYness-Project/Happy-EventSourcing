using HP.Domain.Common;

namespace HP.Domain.Person
{
    public class Person : Entity, IAggregateRoot
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public Email Email { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        protected Person() 
        {
            IsActive = false;
        }
        public Person(string firstName, string lastName, Address address, Email email, string userId = null)
        {

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            UserId = userId;
            IsActive = true;    

            AddDomainEvent(new PersonEvents.PersonCreated(Id, firstName, lastName, email, address));
        }

        public static Person Create(string firstName, string lastName, Address address, string emailValue, string userId= null)
        {
            if (firstName is null || lastName is null)
                throw new ArgumentNullException("Firstname or lastName cannot be null");

            if (address is null)
                throw new ArgumentNullException(nameof(address));

            //var userCreatedEvent = new UserCreatedEvent(user, password);
            Email email = new Email(emailValue);
            return new Person(firstName, lastName, address, email, userId); 
        }

        public static Address CreateAddress(string Country, string City, string Region, string PostalCode)
        {
            // TODO Validation for the Address.
            return new Address(Country, City, Region, PostalCode);
        }

        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case PersonEvents.PersonCreated c:
                    //this.Id = c.AggregateId;
                    break;

                case PersonEvents.PersonUpdated u:
                    break;

                //case PersonEvents.PersonDeleted d:
                //    break;
            }
            throw new NotImplementedException();
        }
    }
}
