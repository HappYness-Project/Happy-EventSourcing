using HP.Domain.Person;
namespace HP.test
{
    public class PersonFactory
    {
        public static Person Create()
        {
            return Person.Create("Kevin", "Park", "hyunbin7303@gmail.com");
        }
        public static Person Create(string fName, string lName, string email)
        {
            return Person.Create(fName, lName, email);
        }
    }
    
}
