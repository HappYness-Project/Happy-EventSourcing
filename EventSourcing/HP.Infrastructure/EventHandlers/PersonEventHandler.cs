using HP.Domain;
using HP.Infrastructure.DbAccess;
using static HP.Domain.PersonDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    // This is Query side
    public class PersonEventHandlers : IPersonEventHandler
    {
        private readonly IMongoDbContext _dbContext;
        #region Ctors
        public PersonEventHandlers(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region handlers
        public async Task On(PersonCreated @event)
        {
            var check = @event;

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
