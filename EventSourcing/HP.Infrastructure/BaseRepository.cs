using HP.Core.Common;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace HP.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AggregateRoot
    {
        protected readonly IMongoCollection<T> _collection;
        public BaseRepository(IMongoDbContext dbContext)
        {
            _collection = dbContext.GetCollection<T>() ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }
        public virtual async Task<long> CountAsync()
        {
            return await _collection.CountDocumentsAsync(f => true);
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

        public Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDelete(filterExpression));
        }
        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            var set = CreateSet();
            return set.Any(predicate);
        }
        private IQueryable<T> CreateSet()
        {
            return _collection.AsQueryable<T>();
        }
        public virtual async Task InsertManyAsync(ICollection<T> documents)
        {
            await _collection.InsertManyAsync(documents);
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

        public Task<List<T>> GetAllByAggregateId(Guid aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}
