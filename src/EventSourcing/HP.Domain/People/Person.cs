using HP.Core.Models;
using static HP.Domain.PersonDomainEvents;

namespace HP.Domain
{
    public class Person : AggregateRoot
    {
        public string Email { get; private set; }
        public string DisplayName { get; private set; } 
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
        public Person() : base() {
            InitSetup();
        }
        public Person(string displayname) : base()
        {
            InitSetup();
            DisplayName = displayname;
            AddDomainEvent(new PersonCreated { AggregateId = Id, DisplayName = displayname, PersonRole = Role.ToString(), PersonType = Type });

        }
        public static Person Create(string? displayName = null)=> new Person(displayName);

        public void UpdateRole(string role)
        {
            if (role is null)
                throw new ArgumentNullException("Role input cannot be null");

            AddDomainEvent(new PersonRoleUpdated { PreRole = Role.ToString(), Role = role });
        }
        public void UpdateGroupId(int groupId)
        {
            this.GroupId = groupId;
            AddDomainEvent(new PersonGroupUpdated { GroupId = GroupId });
        }
        public void UpdateBasicInfo(string? personType, string? goalType)
        {
            this.Type = personType;
            this.GoalType = GoalType.FromName(goalType);
            AddDomainEvent(new PersonInfoUpdated { PersonType = personType, GoalType = goalType });
        }
        public void Remove()
        {
            this.IsActive = false;
            AddDomainEvent(new PersonRemoved { AggregateId = this.Id });    
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

                case PersonGroupUpdated groupupdated:
                    Apply(groupupdated);
                    break;

                case PersonRoleUpdated roleupdated:
                    Apply(roleupdated);
                    break;

                case PersonRemoved removed:
                    Apply(removed);
                    break;
            }
        }
        #region EventApply
        private void Apply(PersonCreated @event)
        {
            Id = @event.AggregateId;
            DisplayName = @event.DisplayName;
            Type = @event.PersonType;
            Role = PersonRole.FromName(@event.PersonRole);
        }
        private void Apply(PersonInfoUpdated @event)
        {
            Id = @event.AggregateId;
            Type = @event.PersonType;
            GoalType = GoalType.FromName(@event.GoalType);
        }
        private void Apply(PersonGroupUpdated @event)
        {
            GroupId = @event.GroupId;   
        }
        private void Apply(PersonRemoved @event)
        {
            IsActive = false;
        }
        private void Apply(PersonRoleUpdated @event)
        {
            Role = PersonRole.FromName(@event.Role);
        }
        #endregion

    }
}
            