using HP.Core.Models;
using static HP.Domain.PersonDomainEvents;

namespace HP.Domain
{
    public class Person : AggregateRoot
    {
        public string PersonName { get; private set; } 
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
        public Person(string personName)
        {
            PersonName = personName;
            IsActive = true;
            CurrentScore = 0;
            GoalType = GoalType.NotDefined;
            Role = PersonRole.TBD.ToString();
            UpdateDate = DateTime.Now;
            AddDomainEvent(new PersonCreated(Id, personName));

        }
        public void UpdateRole(string role)
        {
            if (role is null)
                throw new ArgumentNullException("Role input cannot be null");

            string preRole = this.Role;
            this.Role = role;
            AddDomainEvent(new PersonRoleUpdated(Id, preRole, role));
        }
        public void UpdateGroupId(int groupId)
        {
            this.GroupId = groupId;
            AddDomainEvent(new PersonGroupUpdated(Id, GroupId));
        }
        public static Person Create(string? personName = null)
        {
            return new Person(personName); 
        }
        public void UpdateBasicInfo(string? personType, string? goalType, int? groupId)
        {
            this.PersonType = personType;
            this.GoalType = GoalType.FromName(goalType);
            this.GroupId = groupId.Value;
            AddDomainEvent(new PersonInfoUpdated(Id, personType, goalType));
        }
        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case PersonCreated created:
                    Apply(created);
                    break;

                case PersonInfoUpdated updated:
                    Apply(updated);
                    break;

                case PersonRoleSetAdminAssigned adminAssigned:
                    Apply(adminAssigned);
                    break;

                case PersonGroupUpdated groupupdated:
                    Apply(groupupdated);
                    break;
            }
        }
        private void Apply(PersonCreated @event)
        {
            Id = @event.PersonId;
            PersonName = @event.PersonName;
        }
        private void Apply(PersonInfoUpdated @event)
        {
            Id = @event.PersonId;
        }
        private void Apply(PersonRoleSetAdminAssigned @event)
        {
            Id = @event.PersonId;
        }
        private void Apply(PersonGroupUpdated @event)
        {
            Id = @event.PersonId;
            GroupId = @event.GroupId;   
        }

    }
}
            