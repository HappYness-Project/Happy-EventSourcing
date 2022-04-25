
using HP.Domain.Person;

namespace HP.Application
{
    public interface IDemoDataAccess
    {
        List<Person> GetPeople();
        Person InsertPerson(string firstName, string LastName);
    }
}