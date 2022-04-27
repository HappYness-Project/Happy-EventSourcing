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

        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Todo entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IFindFluent<Todo, Todo> Find(FilterDefinition<Todo> filter)
        {
            throw new NotImplementedException();
        }

        public IFindFluent<Todo, Todo> Find(Expression<Func<Todo, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Todo> FindOneAndReplaceAsync(FilterDefinition<Todo> filter, Todo replacement)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Todo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Todo> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Todo entity)
        {
            throw new NotImplementedException();
        }
    }
}
