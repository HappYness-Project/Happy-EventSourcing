using HP.Domain;
using HP.Domain.People.Read;
using HP.Infrastructure.DbAccess;
namespace HP.Infrastructure.Repository.Read
{
    public class PersonRepository : BaseRepository<PersonDetails>, IPersonRepository
    {
        public PersonRepository(HpReadDbContext dbContext) : base(dbContext) { } 
    }
}
