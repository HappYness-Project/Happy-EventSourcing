using HP.Domain;

namespace HP.UnitTest.People
{
    public class PersonFactory
    {
        public static Person Create()
        {
            Address addr = new Address("Canada", "Waterloo", "ON", "N2L 4m2");
            return Person.Create("hyunbin7303@gmail.com");
        }
        public static Person Create(Address addr, string email)
        {
            return Person.Create(email);
        }
    }

}
