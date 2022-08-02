using HP.Domain.Person;
namespace HP.test
{
    public class PersonFactory
    {
        public static Person Create()
        {
            Address addr = new Address("Canada", "Waterloo","ON","N2L-4m2");
            Email email = new Email("hyunbin7303@gmail.com");
            return Person.Create("Kevin", "Park", addr, email);
        }
        public static Person Create(string fName, string lName, string addr, string email)
        {
            return Person.Create(fName, lName, email);
        }
    }
    
}
