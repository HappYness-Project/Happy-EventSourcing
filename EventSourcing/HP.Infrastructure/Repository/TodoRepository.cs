using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMongoCollection<Todo> _todos;
        public TodoRepository(IMongoDbContext dbContext) : base(dbContext)
        {
            _todos = dbContext.GetCollection<Todo>();
        }

        public Task<IEnumerable<Todo>> GetListByTags(string[] tags)
        {
            //var check = _todos.AsQueryable().Where(x => tags.Contains(x.Tag));
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Todo>> GetListByUserId(string userId)
        {
            return await _todos.AsQueryable().Where(x => x.UserId == userId).ToListAsync();
        }

        public IEnumerable<Todo> Search(int page, int recordsPerPage, string TodoTitle, out int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
