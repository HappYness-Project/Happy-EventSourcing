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
        public TodoRepository(IMongoDbContext dbContext, IEventStore eventStore) : base(dbContext)
        {
            _todos = dbContext.GetCollection<Todo>();
        }
        public async Task<Todo> GetActiveTodoById(string todoId)
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
//         public async Task<IEnumerable<Todo>> GetListByScoreOfUser(string userId, string targetScore)
//         {
//             var highExamScoreFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>
//             (
//                 "scores", new BsonDocument { { "type", "exam" },
//                 { "score", new BsonDocument { { "$gte", 95 } } }
//             });
//   //          _todos.Find(highExamScoreFilter);

//         }
        public IEnumerable<Todo> Search(int page, int recordsPerPage, string TodoTitle, out int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
