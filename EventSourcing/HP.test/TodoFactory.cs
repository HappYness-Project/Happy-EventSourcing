using HP.Domain;
namespace HP.test
{
    public class TodoFactory
    {
        public static Todo Create()
        {
            return Todo.Create(new Person("Kevin", "Park", new Address("Canada", "Waterloo", "Ontario", "n2l-4m2"), new Email("hyunbin7303@gmail.com")), "Create Todo through the UnitTest", "description","General", null);
        }
        public static Todo Create(string userId, string todoTitle, bool defaultTag = true)
        {
            string[] tags = { "Study", "Exercise", "Chore" };
            Person person = Person.Create("Kevin", "Park", new Address("Canada", "Waterloo", "Ontario", "n2l-4m2"), "hyunbin7303@gmail.com");
            return Todo.Create(person, todoTitle,"Description", "General", defaultTag ? tags : null);
        }
    }
}
