using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.Todos
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        Task<IEnumerable<Todo>> GetListByKey(string key, string userId = null);
        Task<IEnumerable<Todo>> GetListByUserId(string userId);
    }
}
