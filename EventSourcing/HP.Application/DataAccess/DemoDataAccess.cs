
using HP.Domain.Person;

namespace HP.Application
{
    public class DemoDataAccess : IDemoDataAccess
    {
        private List<Person> people = new();
        public DemoDataAccess()
        {
            people.Add(new Person() { Id = 1, FirstName = "Kevin", LastName = "Park" });
            people.Add(new Person() { Id = 2, FirstName = "Julio", LastName = "Rivas" });
            people.Add(new Person() { Id = 3, FirstName = "Adam", LastName = "Sosnek" });
        }
        public List<Person> GetPeople() { return people; }
        public Person InsertPerson(string firstName, string LastName)
        {
            Person p = new() { FirstName = firstName, LastName = LastName };
            p.Id = people.Max(x => x.Id) + 1;
            people.Add(p);
            return p;
        }
    }
}