using DemoLib.Models;

namespace DemoLib
{
    public class DemoDataAccess : IDemoDataAccess
    {
        private List<PersonModel> people = new();
        public DemoDataAccess()
        {
            people.Add(new PersonModel() { Id = 1, FirstName = "Kevin", LastName = "Park" });
            people.Add(new PersonModel() { Id = 2, FirstName = "Julio", LastName = "Rivas" });
            people.Add(new PersonModel() { Id = 3, FirstName = "Adam", LastName = "Sosnek" });
        }
        public List<PersonModel> GetPeople() { return people; }
        public PersonModel InsertPerson(string firstName, string LastName)
        {
            PersonModel p = new() { FirstName = firstName, LastName = LastName };
            p.Id = people.Max(x => x.Id) + 1;
            people.Add(p);
            return p;
        }
    }
}