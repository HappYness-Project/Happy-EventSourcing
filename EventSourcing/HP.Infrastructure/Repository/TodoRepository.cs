using HP.Domain;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
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
        public TodoRepository(IMongoDbContext dbContext, IEventStore eventStore) : base(dbContext)
        {
            _todos = dbContext.GetCollection<Todo>();
        }

        public Task<IEnumerable<Todo>> GetListByTags(string[] tags)
        {
            // FilterDefinition.
            foreach (var tag in tags)
            {
                var filter = Builders<Todo>.Filter.Eq("Tags.", tag);
                var check = _todos.Find(filter);
            }
            //await _collection.InsertOneAsync(entity);
            //return entity;
            throw new NotImplementedException();
        }
        //https://www.mongodb.com/blog/post/quick-start-c-and-mongodb-read-operations
        public async Task<IEnumerable<Todo>> GetListByUserId(string userId)
        {
            var filter = Builders<Todo>.Filter.Eq("UserId", userId);
            var test = _todos.Find(filter).ToList();
            //return await _todos.AsQueryable().Where(x => x.UserId == userId).ToListAsync();
            return test;    
        }

        public IEnumerable<Todo> Search(int page, int recordsPerPage, string TodoTitle, out int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
