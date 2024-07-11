using FluentAssertions;
using HP.Core.Common;
using HP.Domain;
using HP.Infrastructure.Repository.Write;
using NUnit.Framework;
namespace HP.IntegrationTest
{
    [Category("Integration")]
    public class PersonRepositoryTest : TestBase
    {
        private IAggregateRepository<Person> personRepository = null;

        [SetUp]
        public void Setup()
        {
            personRepository = new AggregateRepository<Person>(_eventStore);
        }


        [Test]
        public void GivenNoEvent_WhenPersistCalled_ThenNothing()
        {
            var newPerson = Person.Create("hyunbin7303@gmail.com", "hyunbin7303");

            personRepository.PersistAsync(newPerson);

            Assert.That(true);
        }

    }
}