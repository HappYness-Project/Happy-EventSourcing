using HP.Core.Common;
using HP.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace HP.Infrastructure.Repository
{
    public class BaseAggregateRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;
        public BaseAggregateRepository(IMongoDbContext dbContext)
        {
            _collection = dbContext.GetCollection<T>() ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }
        public virtual async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(p => p.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = false });
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
        public virtual async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await _collection.AsQueryable().AnyAsync(predicate);
        }
        public Task DeleteByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }
        public virtual Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }
        public IList<T> FindAll(Expression<Func<T, bool>> filterExpression)
        {
            return _collection.AsQueryable().Where(filterExpression.Compile()).ToList();
        }
    }
}
