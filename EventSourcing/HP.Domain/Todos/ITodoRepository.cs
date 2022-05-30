using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.Todos
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        Task<IEnumerable<Todo>> GetListByUserId(string userId);
        Task<IEnumerable<Todo>> GetListByTags(string[] tags);
        IEnumerable<Todo> Search(int page, int recordsPerPage, string TodoTitle, out int totalCount);

    }
}
