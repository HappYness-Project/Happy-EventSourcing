using HP.Core.Events;
using HP.Domain;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
namespace HP.Infrastructure.Repository
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        private readonly IMongoCollection<Todo> _todos;
        public TodoRepository(IMongoDbContext dbContext) : base(dbContext){ }
        public async Task<Todo> GetActiveTodoById(Guid todoId)
        {
            return await _todos.Find(x => x.Id == todoId && x.IsActive).FirstOrDefaultAsync();
        }
        public Task<IEnumerable<Todo>> GetListByTags(string[] tags)
        {
            foreach (var tag in tags)
            {
                var filter = Builders<Todo>.Filter.Eq("Tags.", tag);
                var check = _todos.Find(filter);
            }
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Todo>> GetListByUserId(string userId)
        {
            var filter = Builders<Todo>.Filter.Eq("UserId", userId.ToUpper());
            var todos = await _todos.FindAsync(filter).Result.ToListAsync();
            return todos;
        }
        public IEnumerable<Todo> Search(int page, int recordsPerPage, string TodoTitle, out int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
