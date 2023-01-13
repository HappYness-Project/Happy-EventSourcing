using HP.Core.Events;
using HP.Domain;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
namespace HP.Infrastructure.Repository
{
    public class TodoRepository : BaseAggregateRepository<Todo>, ITodoAggregateRepository
    {
        public TodoRepository(IMongoDbContext dbContext) : base(dbContext){ }
        public async Task<Todo> GetActiveTodoById(Guid todoId)
        {
            return await _collection.Find(x => x.Id == todoId && x.IsActive).FirstOrDefaultAsync();
        }
        public Task<IEnumerable<Todo>> GetListByTags(string[] tags)
        {
            foreach (var tag in tags)
            {
                var filter = Builders<Todo>.Filter.Eq("Tags.", tag);
                var check = _collection.Find(filter);
            }
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Todo>> GetListByPersonName(string personName)
        {
            var filter = Builders<Todo>.Filter.Eq("PersonName", personName.ToUpper());
            var todos = await _collection.FindAsync(filter).Result.ToListAsync();
            return todos;
        }
        public IEnumerable<Todo> Search(int page, int recordsPerPage, string TodoTitle, out int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
