using HP.Domain.Common;
namespace HP.Domain
{
    public class Person : Entity, IAggregateRoot
    {
        public string UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }
        public string Description { get; private set; }
        public int GroupId { get; private set; }
        public int ProjectId { get; private set; }
        public string Role { get; private set; }
        public GoalType GoalType { get; private set; }
        public bool IsActive { get; private set; }
        public decimal CurrentScore { get; private set; }
        public DateTime UpdateDate { get; private set; }
        protected Person() 
        {
            IsActive = false;
            CurrentScore = 0;
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
            GoalType = GoalType.NotDefined;
            Role = PersonRoleType.Normal.ToString(); // For now, Normal is the default Role.
            AddDomainEvent(new PersonDomainEvents.PersonCreated(Id, firstName, lastName, email, address));
        }
        public void UpdateRole(string role)
        {
            if (role is null)
                throw new ArgumentNullException("Role input cannot be null");

            string preRole = this.Role;
            this.Role = role;
            AddDomainEvent(new PersonDomainEvents.PersonRoleUpdated(Id, preRole, role));
        }
        public void UpdateGroupId(int groupId)
        {
            this.GroupId = groupId;
            AddDomainEvent(new PersonDomainEvents.PersonGroupUpdated(Id, this.GroupId));
        }
        public static Person Create(string firstName, string lastName, Address address, string emailValue, string userId= null)
        {
            if (firstName is null || lastName is null)
                throw new ArgumentNullException("Firstname or lastName cannot be null");

            if (address is null)
                throw new ArgumentNullException(nameof(address));

            Email email = new Email(emailValue);
            return new Person(firstName.ToUpper(), lastName.ToUpper(), address, email, userId); 
        }
        public static Person UpdateBasicPerson(Person person, string firstName, string lastName, string emailAddr)
        {
            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = new Email(emailAddr);
            return person;
        }
        public static Address CreateAddress(string Country, string City, string Region, string PostalCode)
        {
            return new Address(Country, City, Region, PostalCode);
        }
        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case PersonDomainEvents.PersonCreated created:
                    Id = created.AggregateId.ToString();
                    break;

                case PersonDomainEvents.PersonUpdated u:
                    
                    break;

                case PersonDomainEvents.PersonRoleSetAdminAssigned a:

                    break;

                case PersonDomainEvents.PersonGroupUpdated d:
                    break;
            }
        }
    }
}
            