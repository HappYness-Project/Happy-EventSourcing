using HP.Core.Models;
namespace HP.Domain
{
    public class Person : AggregateRoot<string>
    {
        public string PersonId { get; private set; } 
        public string PersonType { get; private set; }
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
        public Person(string personId)
        {
            PersonId = personId;
            IsActive = true;
            GoalType = GoalType.NotDefined;
            Role = PersonRoleType.Normal.ToString(); // For now, Normal is the default Role.
            AddDomainEvent(new PersonDomainEvents.PersonCreated(Id));
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
        public static Person Create(string userId= null)
        {
            return new Person(userId); 
        }
        public void UpdateBasicInfo(string PersonType, int? GroupId)
        {
            // Todo : Do Api call or generate Kafka data.
            this.PersonType = PersonType;
            this.GroupId = GroupId.Value;
        }
        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case PersonDomainEvents.PersonCreated created:
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
            