using FluentAssertions;
using HP.Core.Common;
using HP.Domain;
using HP.Infrastructure.Repository.Write;
using NUnit.Framework;
namespace HP.IntegrationTest
{
    public class PersonRepositoryTest : TestBase
    {
        private IAggregateRepository<Person> personRepository = null;

        [SetUp]
        public void Setup()
        {
            personRepository = new AggregateRepository<Person>(_eventStore);
        }

        [Test]
        public void GetAggregateIdAsync()
        {

            var people = personRepository.GetAggregateIdAsync();
            people.Should().NotBeNull();
        }

        [Test]
        public void GetTesting()
        {
            personRepository.GetByAggregateId
        }
    }
}