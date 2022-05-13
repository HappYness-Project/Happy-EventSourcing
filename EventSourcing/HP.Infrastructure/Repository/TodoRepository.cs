using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        private readonly IMongoCollection<Todo> _todoCollections;
        public TodoRepository(IMongoDbContext dbContext) : base(dbContext)
        {
            _todoCollections = dbContext.GetCollection<Todo>();
        }
        public Task<IEnumerable<Todo>> GetTodosWithKey(string key)
        {
            throw new NotImplementedException();
        }

    }
}
