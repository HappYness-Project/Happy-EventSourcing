using HP.Core.Common;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace HP.Infrastructure
{
    public class BaseAggregateRepository<T> : IBaseRepository<T> where T : AggregateRoot
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
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            var set = _collection.AsQueryable<T>();
            return set.Any(predicate);
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
            return _collection.AsQueryable<T>().Where(filterExpression.Compile()).ToList();
        }
    }
}
