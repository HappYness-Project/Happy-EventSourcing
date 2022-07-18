using HP.Domain.Todos;
namespace HP.test
{
    public class TodoFactory
    {
        public static Todo Create()
        {
            return Todo.Create("userId7303", "Create Todo through the UnitTest", "description","General", null);
        }
        public static Todo Create(string userId, string todoTitle, bool defaultTag = true)
        {
            string[] tags = { "Study", "Exercise", "Chore" };
            return Todo.Create(userId, todoTitle,"Description", "General", defaultTag ? tags : null);
        }
    }
}
