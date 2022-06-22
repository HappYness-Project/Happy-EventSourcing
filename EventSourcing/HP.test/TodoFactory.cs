using HP.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.test
{
    public class TodoFactory
    {
        public static Todo Create()
        {
            return Todo.CreateTodo("userId7303", "Create Todo through the UnitTest", "General", null);
        }
        public static Todo Create(string userId, string todoTitle, bool defaultTag = true)
        {
            string[] tags = { "Study", "Exercise", "Chore" };
            return Todo.CreateTodo(userId, todoTitle, "General", defaultTag ? tags : null);
        }
    }
}
