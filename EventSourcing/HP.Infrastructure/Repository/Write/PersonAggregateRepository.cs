using HP.Domain;
using HP.Infrastructure.DbAccess;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HP.Infrastructure.Repository.Write
{
    public class PersonAggregateRepository : BaseAggregateRepository<Person>, IPersonAggregateRepository
    {
        public PersonAggregateRepository(IMongoDbContext dbContext) : base(dbContext) { }
        public Task<bool> DeletePersonAsync(Guid personId)
        {
            var check = _collection.DeleteOne(x => x.Id == personId);
            return Task.FromResult(check.DeletedCount > 0 ? true : false);
        }
        public async Task<Person> GetPersonByPersonNameAsync(string personName)
        {
            return await _collection.Find(x => x.PersonName == personName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetListByGroupIdAsync(int groupId)
        {
            return await _collection.Find(x => x.GroupId == groupId).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetListByRoleAsync(string role)
        {
            return await _collection.Find(x => x.Role == role).ToListAsync();
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            // TODO :Requred to update this method for updating Person.
            var filter = Builders<Person>.Filter.And(Builders<Person>.Filter.Eq("UserId", person.PersonName));
            var update = Builders<Person>.Update.Set("FirstName", "")
                                                .Set("UpdateDate", DateTime.Now);
            var result = await _collection.FindOneAndUpdateAsync(filter, update,
                    options: new FindOneAndUpdateOptions<Person, BsonDocument>
                    {
                        IsUpsert = true,
                        ReturnDocument = ReturnDocument.After
                    });
            return person;
        }
    }
}
