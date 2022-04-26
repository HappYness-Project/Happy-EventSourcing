using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain
{
    public interface IBaseRepository<T> : IDisposable where T : Entity
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetListAsync();
        Task<long> CountAsync();
    }
}
