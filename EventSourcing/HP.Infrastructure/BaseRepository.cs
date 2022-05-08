using HP.Domain;
using HP.Domain.Common;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace HP.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly IMongoCollection<T> _collection;
        public BaseRepository(IMongoDbContext dbContext)
        {
            _collection = dbContext.GetCollection<T>();
        }
        public virtual async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public virtual async Task<long> CountAsync()
        {
            return await _collection.CountDocumentsAsync(f => true);
        }
        public virtual async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(p => p.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = false });
        }
        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T replacement)
        {
            return _collection.FindOneAndReplaceAsync(filter, replacement);
        }

        public IFindFluent<T, T> Find(FilterDefinition<T> filter)
        {
            return _collection.Find(filter);
        }
        public IFindFluent<T, T> Find(Expression<Func<T, bool>> filter)
        {
            return _collection.Find(filter);
        }
    }
}
