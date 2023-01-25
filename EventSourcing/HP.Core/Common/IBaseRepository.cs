using System.Linq.Expressions;
namespace HP.Core.Common
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);
        public bool Exists(Expression<Func<T, bool>> predicate);
    }
}
