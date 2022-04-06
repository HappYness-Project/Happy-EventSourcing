
using HP.Domain.Person;

namespace DemoLib
{
    public interface IDemoDataAccess
    {
        List<Person> GetPeople();
        Person InsertPerson(string firstName, string LastName);
    }
}