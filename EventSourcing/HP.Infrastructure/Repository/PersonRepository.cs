using HP.Domain;
using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IMongoCollection<Person> _mongoCollection;
        public PersonRepository(IMongoDbContext dbContext) : base(dbContext)
        {
            this._mongoCollection = dbContext.GetCollection<Person>() ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePersonAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IFindFluent<Person, Person> Find(FilterDefinition<Person> filter)
        {
            throw new NotImplementedException();
        }

        public IFindFluent<Person, Person> Find(Expression<Func<Person, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Person> FindOneAndReplaceAsync(FilterDefinition<Person> filter, Person replacement)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePersonAsync(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
