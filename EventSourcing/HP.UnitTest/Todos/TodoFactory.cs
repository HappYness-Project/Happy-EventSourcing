using HP.Domain;
namespace HP.test
{
    public class TodoFactory
    {
        public static Todo Create()
        {
            return Todo.Create(new Person("UserId"), "Title", "Desc", TodoType.Others, null);
        }
        public static Todo Create(string userId, string todoTitle, string type, string desc, bool defaultTag = true)
        {
            string[] tags = { "Study", "Exercise", "Chore" };
            Person person = Person.Create(userId);
            TodoType Type = TodoType.FromName(type);
            return Todo.Create(person, todoTitle, desc, Type, defaultTag ? tags : null);
        }
    }
}
