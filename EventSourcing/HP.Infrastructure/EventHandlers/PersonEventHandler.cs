using HP.Domain;
using static HP.Domain.PersonDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    // This is Query side
    public class PersonEventHandler : IPersonEventHandler
    {
        private readonly IPersonRepository _personRepository;
        #region Ctors
        public PersonEventHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        #endregion

        #region handlers
        public Task On(PersonCreated @event)
        {
            throw new NotImplementedException();
        }

        public Task On(PersonInfoUpdated @event)
        {
            throw new NotImplementedException();
        }

        public Task On(PersonGroupUpdated @event)
        {
            throw new NotImplementedException();
        }

        public Task On(PersonRoleSetAdminAssigned @event)
        {
            throw new NotImplementedException();
        }

        public Task On(PersonRoleUpdated @event)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
