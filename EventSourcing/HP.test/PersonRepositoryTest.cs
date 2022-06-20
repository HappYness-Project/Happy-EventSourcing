using HP.Domain.Person;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;

namespace HP.test
{
    public class PersonRepositoryTest : TestBase
    {
        IPersonRepository personRepository = null;
        // Repository pattern testing
        [SetUp]
        public void Setup()
        {
            personRepository = new PersonRepository(mongoDbContext, null);
        }

        [Test]
        public void GetAllAsync_ReturnAllPeople()
        {
            var people = personRepository.GetAllAsync();
            Assert.NotNull(people);
        }
        [Test]
        public void Delete_Specific_person_by_PersonId()
        {
            // Creating a person object into the db.
            Person person = Person.Create("Kevin", "Park", "asdf@gmail.com");
            var newPerson = personRepository.CreateAsync(person).Result;

            var isRemoved = personRepository.DeletePersonAsync(newPerson.Id)?.Result;
            Assert.IsTrue(isRemoved);
        }

        [Test]
        public void CreatePerson()
        {
            Person person = Person.Create("Kevin", "Park", "hyunbin7303@gmail.com");
            var personObj = personRepository.CreateAsync(person)?.Result;
            Assert.That(personObj, Is.Not.Null);
        }
    }
}