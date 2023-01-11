using HP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HP.Core.Common
{
    public interface IEntityBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);
        Task DeleteByIdAsync(Guid id);

    }
}
