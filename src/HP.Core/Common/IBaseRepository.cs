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
        IList<T> FindAll(Expression<Func<T, bool>> filterExpression);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
