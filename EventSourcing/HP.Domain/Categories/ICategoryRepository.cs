using HP.Core.Common;
using HP.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain
{
    public interface ICategoryRepository : IAggregateBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesByUserId(string userId);
    }
}
 