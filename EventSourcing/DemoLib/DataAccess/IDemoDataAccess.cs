using DemoLib.Models;

namespace DemoLib
{
    public interface IDemoDataAccess
    {
        List<PersonModel> GetPeople();
        PersonModel InsertPerson(string firstName, string LastName);
    }
}