using HP.Domain;
using HP.Domain.People.Read;
using HP.Infrastructure.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository.Read
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IMongoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
