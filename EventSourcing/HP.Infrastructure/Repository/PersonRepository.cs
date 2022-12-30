using HP.Core.Events;
using HP.Domain;
using HP.Infrastructure.DbAccess;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HP.Infrastructure.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IMongoCollection<Person> _mongoCollection;
        public PersonRepository(IMongoDbContext dbContext, IEventStore eventStore) : base(dbContext)
        {
            this._mongoCollection = dbContext.GetCollection<Person>() ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public Task<bool> DeletePersonAsync(Guid personId)
        {
            var check = _mongoCollection.DeleteOne(x => x.Id == personId);
            return Task.FromResult(check.DeletedCount > 0 ? true : false);
        }
        public async Task<Person> GetPersonByUserIdAsync(string UserId)
        {
            return await _mongoCollection.Find(x => x.PersonName == UserId).FirstOrDefaultAsync();
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
            // TODO :Requred to update this method for updating Person.
            var filter = Builders<Person>.Filter.And(Builders<Person>.Filter.Eq("UserId", person.PersonName));
            var update = Builders<Person>.Update.Set("FirstName", "")
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
