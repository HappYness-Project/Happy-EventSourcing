using FluentAssertions;
using HP.Core.Events;
using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using HP.test;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections;

namespace HP.UnitTest.People
{
    public class PersonRepositoryTest : TestBase
    {
        private IPersonAggregateRepository personRepository = null;
        [SetUp]
        public void Setup()
        {
            personRepository = new PersonRepository(_mongoDbContext);
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
            Address addr = new Address("Canada", "Waterloo", "ON", "n2l4m2");
            Person person = Person.Create("hyunbin7303@gmail.com");
            var newPerson = personRepository.CreateAsync(person).Result;

            var isRemoved = personRepository.DeletePersonAsync(newPerson.Id)?.Result;
            Assert.IsTrue(isRemoved);
        }

        [Test]
        public void CreatePerson()
        {
            // Arrange
            Person person = Person.Create("hyunbin7303@gmail.com");

            var personObj = personRepository.CreateAsync(person)?.Result;
            Assert.That(personObj, Is.Not.Null);
        }

        [Test]
        public void UpdatePersonAsync_UpdateSuccessful()
        {
            // Arrange
            Person person = Person.Create("hyunbin7303");
            var personObj = personRepository.UpdatePersonAsync(person)?.Result;
            Assert.That(personObj, Is.Not.Null);
        }
        [Test]
        public void GetListByRoleAsync_return_NormalRolePerson()
        {
            var personObj = personRepository.GetListByRoleAsync("normal").Result;
            personObj.Should().NotBeNull();
        }
    }
}