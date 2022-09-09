using HP.Domain;
using HP.Infrastructure.DbAccess;
using MongoDB.Bson;
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
      //  private readonly IEventStore _eventStore;
        public PersonRepository(IMongoDbContext dbContext, IEventStore eventStore) : base(dbContext)
        {
            this._mongoCollection = dbContext.GetCollection<Person>() ?? throw new ArgumentNullException(nameof(dbContext));
      //      _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));   
        }
        public Task<bool> DeletePersonAsync(string personId)
        {
            var check = _mongoCollection.DeleteOne(x => x.Id == personId);
            return Task.FromResult(check.DeletedCount > 0 ? true : false);
        }
        public async Task<Person> GetPersonByUserIdAsync(string UserId)
        {
            return await _mongoCollection.Find(x => x.UserId == UserId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetListByGroupIdAsync(int groupId)
        {
            return await _mongoCollection.Find(x => x.GroupId == groupId).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetListByRoleAsync(string role)
        {
            return await _mongoCollection.Find(x => x.Role == role).ToListAsync();
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            var filter = Builders<Person>.Filter.And(Builders<Person>.Filter.Eq("UserId", person.UserId));
            var update = Builders<Person>.Update.Set("FirstName", person.FirstName)
                                                .Set("LastName", person.LastName)
                                                .Set("Email.EmailAddr", person.Email.ToString())
                                                .Set("UpdateDate", DateTime.Now);
            var result = await _mongoCollection.FindOneAndUpdateAsync(filter, update,
                    options: new FindOneAndUpdateOptions<Person, BsonDocument>
                    {
                        IsUpsert = true,
                        ReturnDocument = ReturnDocument.After
                    });
            return person;
        }
    }
}
