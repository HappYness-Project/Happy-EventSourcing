using HP.Domain;
namespace HP.test
{
    public class TodoFactory
    {
        public static Todo Create()
        {
            return Todo.Create(new Person("UserId"), "Title", "Desc", TodoType.Others, null);
        }
        public static Todo Create(string userId, string todoTitle, string desc, bool defaultTag = true)
        {
            string[] tags = { "Study", "Exercise", "Chore" };
            Person person = Person.Create(userId);
            return Todo.Create(person, todoTitle, desc, TodoType.Others, defaultTag ? tags : null);
        }
    }
}
