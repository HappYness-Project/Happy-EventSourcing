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
            Address = address;
            Email = email;
            UserId = userId;
            IsActive = true;    


        }

        public static Person Create(string firstName, string lastName, Address address, string emailvalue, string userId=null)
        {
            //if (!email.IsValid) throw new EmailNotValidException(email);
            //bool unique = userUniqueChecker.CheckAsync(email, cancellationToken).GetAwaiter().GetResult();
            //if (!unique) throw new UserAlreadyExistException(email);
            //UserId userId = userIdGenerator.Generate();
            //Password password = passwordGenerator.Generate();
            //PasswordHash passwordHash = passwordHasher.Hash(password);
            //var user = new User(userId, email, passwordHash, DateTime.UtcNow);
            //var userCreatedEvent = new UserCreatedEvent(user, password);
            Email email = new Email(emailvalue);
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
