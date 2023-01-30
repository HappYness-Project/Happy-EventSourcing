using HP.Domain;
using HP.Domain.People.Read;
using HP.Infrastructure.DbAccess;
using static HP.Domain.PersonDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    // This is Query side
    public class PersonEventHandlers : IPersonEventHandler
    {
        private readonly IPersonRepository _personRepository;
        #region Ctors
        public PersonEventHandlers(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        #endregion

        #region handlers
        public async Task On(PersonCreated @event)
        {
            PersonDetails personDetails = new PersonDetails(@event.PersonId)
            {
                PersonName = @event.PersonName,
                PersonType = @event.PersonType,
                PersonRole = @event.PersonRole,
            };
            _personRepository.CreateAsync(personDetails);
        }
        public async Task On(PersonInfoUpdated @event)
        {
            throw new NotImplementedException();
        }

        public async Task On(PersonGroupUpdated @event)
        {
            throw new NotImplementedException();
        }

        public async Task On(PersonRoleSetAdminAssigned @event)
        {
            throw new NotImplementedException();
        }

        public async Task On(PersonRoleUpdated @event)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
