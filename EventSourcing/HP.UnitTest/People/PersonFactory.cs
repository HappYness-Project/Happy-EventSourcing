using HP.Domain;

namespace HP.UnitTest.People
{
    public class PersonFactory
    {
        public static Person Create()
        {
            return Person.Create("hyunbin7303@gmail.com");
        }
        public static Person Create(string email)
        {
            return Person.Create(email);
        }
    }

}
