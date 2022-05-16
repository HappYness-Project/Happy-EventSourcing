﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.Todos
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        Task<IEnumerable<Todo>> GetTodosWithKey(string key);

    }
}