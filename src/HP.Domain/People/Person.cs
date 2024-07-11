using HP.Core.Exceptions;
using HP.Core.Models;
using HP.Domain.Exceptions;
using HP.Domain.People;
using static HP.Domain.PersonDomainEvents;

namespace HP.Domain
{
    public class Person : AggregateRoot
    {
        public Email Email { get; private set; }
        public string DisplayName { get; private set; } 
        public string Type { get; private set; } = "Normal";
        public string Description { get; private set; } = string.Empty;
        public int GroupId { get; private set; } = 0;
        public int ProjectId { get; private set; } = 0;
        public PersonRole Role { get; private set; } = PersonRole.TBD;
        public GoalType GoalType { get; private set; } = GoalType.TBD;
        public bool IsActive { get; private set; } = true;
        public decimal CurrentScore { get; private set; } = 0;
        public DateTime UpdateDate { get; private set; } = DateTime.Now;

        public Person() : base() {}
        public Person(Email email, string displayname) : base()
        {
            Email = email;
            DisplayName = displayname;
            AddDomainEvent(new PersonCreated { AggregateId = Id, Email = email, DisplayName = displayname, PersonRole = Role.ToString(), PersonType = Type });

        }
        public static Person Create(string email, string displayName)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("Email cannot be null");
            if(string.IsNullOrWhiteSpace(displayName)) throw new ArgumentNullException("DisplayName cannot be null");

            return new Person(new Email(email), displayName);
        }
        public void UpdateRole(string role)
        {
            if (role is null)
                throw new ArgumentNullException("Role input cannot be null");

            if(Role.ToString() == role.ToLowerInvariant())
                throw new PersonDomainException("Same Role.");
               
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
            