using HP.Core.Common;
using HP.Domain.People.Read;
using static HP.Domain.PersonDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    public class PersonEventHandlers : IPersonEventHandler
    {
        private readonly IBaseRepository<PersonDetails> _personRepository;
        #region Ctors
        public PersonEventHandlers(IBaseRepository<PersonDetails> personRepository)
        {
            _personRepository = personRepository;
        }
        #endregion

        #region handlers
        public async Task On(PersonCreated @event)
        {
            PersonDetails personDetails = new PersonDetails(@event.AggregateId)
            {
                PersonName = @event.PersonName.ToUpper(),
                PersonType = @event.PersonType,
                PersonRole = @event.PersonRole,
            };
            await _personRepository.CreateAsync(personDetails);
        }
        public async Task On(PersonInfoUpdated @event)
        {
            var person = await _personRepository.GetByIdAsync(@event.AggregateId);
            if (person == null) return;

            person.PersonType = @event.PersonType;
            person.GoalType = @event.GoalType;
            person.UpdatedTime = DateTime.UtcNow;
            await _personRepository.UpdateAsync(person);
        }

        public async Task On(PersonGroupUpdated @event)
        {
            var person = await _personRepository.GetByIdAsync(@event.AggregateId);
            if (person == null) return;

            person.GroupId = @event.GroupId;
            person.UpdatedTime = DateTime.UtcNow;
            await _personRepository.UpdateAsync(person);
        }

        public async Task On(PersonRoleUpdated @event)
        {
            var person = await _personRepository.GetByIdAsync(@event.AggregateId);
            if (person == null) return;

            person.PersonRole = @event.Role;
            person.UpdatedTime = DateTime.UtcNow;
            await _personRepository.UpdateAsync(person);
        }
        #endregion
    }
}
