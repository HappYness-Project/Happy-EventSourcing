using HP.Domain.Person;
namespace HP.test
{
    public class PersonFactory
    {
        public static Person Create()
        {
            Address addr = new Address("Canada", "Waterloo","ON","N2L-4m2");
            return Person.Create("Kevin", "Park", addr, "hyunbin7303@gmail.com");
        }
        public static Person Create(string fName, string lName, Address addr, string email)
        {
            return Person.Create(fName, lName, addr, email);
        }
    }
    
}
