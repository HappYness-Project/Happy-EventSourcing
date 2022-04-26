using HP.Domain;
using HP.Domain.Common;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;

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
        public virtual async Task<IEnumerable<T>> GetListAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
