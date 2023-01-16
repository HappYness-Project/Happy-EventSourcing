using HP.Domain;
using HP.Domain.People.Read;
using HP.Infrastructure.DbAccess;


namespace HP.Infrastructure.Repository.Read
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        // How to setup mongo DB Collection.
        public PersonRepository(IMongoDbContext dbContext) : base(dbContext)
        {
        } 
    }
}
