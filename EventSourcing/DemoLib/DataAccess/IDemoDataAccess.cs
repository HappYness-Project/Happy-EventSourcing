using DemoLib.Models;

namespace DemoLib
{
    public interface IDemoDataAccess
    {
        List<Person> GetPeople();
        Person InsertPerson(string firstName, string LastName);
    }
}