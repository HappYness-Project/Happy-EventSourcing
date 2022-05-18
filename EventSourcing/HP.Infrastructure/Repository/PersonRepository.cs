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
        public Task<bool> DeletePersonAsync(string personId)
        {
            var check = _mongoCollection.DeleteOne(personId);
            return Task.FromResult(check.DeletedCount > 0 ? true : false);
        }

        public Task<IEnumerable<Person>> GetListByGroupIdAsync(string groupId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetListByRoleAsync(string role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetListByTagAsync(string tag)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePersonAsync(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
