using HP.Core.Models;
using static HP.Domain.PersonDomainEvents;

namespace HP.Domain
{
    public class Person : AggregateRoot
    {
        public string PersonName { get; private set; } 
        public string Type { get; private set; }
        public string Description { get; private set; }
        public int GroupId { get; private set; }
        public int ProjectId { get; private set; }
        public PersonRole Role { get; private set; }
        public GoalType GoalType { get; private set; }
        public bool IsActive { get; private set; }
        public decimal CurrentScore { get; private set; }
        public DateTime UpdateDate { get; private set; }
        private void InitSetup()
        {
            IsActive = true;
            Description = string.Empty;
            CurrentScore = 0;
            ProjectId = 0;
            GroupId = 0;
            GoalType = GoalType.TBD;
            Role = PersonRole.TBD;
            Type = "Normal";
            UpdateDate = DateTime.Now;
        }
        public Person() : base() { }
        public Person(string personName) : base()
        {
            InitSetup();
            PersonName = personName;
            AddDomainEvent(new PersonCreated { PersonId = Id, PersonName = personName, PersonRole = Role.ToString(), PersonType = Type });

        }
        public static Person Create(string? personName = null)
        {
            return new Person(personName);
        }
        public void UpdateRole(string role)
        {
            if (role is null)
                throw new ArgumentNullException("Role input cannot be null");

            string preRole = Role.ToString();
            Role = PersonRole.FromName(role);
            AddDomainEvent(new PersonRoleUpdated { PersonId = Id, PreRole = preRole, Role = role });
        }
        public void UpdateGroupId(int groupId)
        {
            this.GroupId = groupId;
            AddDomainEvent(new PersonGroupUpdated { PersonId = Id, GroupId = GroupId });
        }
        public void UpdateBasicInfo(string? personType, string? goalType, int? groupId)
        {
            this.Type = personType;
            this.GoalType = GoalType.FromName(goalType);
            this.GroupId = groupId.Value;
            AddDomainEvent(new PersonInfoUpdated { PersonId = Id, PersonType = personType, GoalType = goalType });
        }
        public void Remove()
        {
            this.IsActive = false;
            AddDomainEvent(new PersonRemoved { PersonId = Id });    
        }
        public override void When(IDomainEvent @event)
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

                case PersonRemoved removed:
                    Apply(removed);
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
        private void Apply(PersonRemoved @event)
        {
            Id = @event.PersonId;
            IsActive = false;
        }

    }
}
            