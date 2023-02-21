using FluentAssertions;
using HP.Domain;
using NUnit.Framework;
using System.Linq;

namespace HP.UnitTest.People
{
    public class PersonCreateTests
    {
        [Test]
        public void Create_Should_Create_Person()
        {
            // Arrange 
            string personName = "hyunbin7303";

            // Act
            var fakePerson = Person.Create(personName);

            // Assert
            fakePerson.UncommittedEvents.Should().NotBeNull();
            fakePerson.UncommittedEvents.Count().Should().Be(1);    
            fakePerson.Id.Should().NotBeEmpty();
            fakePerson.Role.Should().Be(PersonRole.TBD);
            fakePerson.CurrentScore.Should().Be(0);
        }

        [Test]
        public void Create_Same_PersonName_Should_Fail()
        {
            // Arrange 
            string personName = "hyunbin7303";

            // Act
            var fakePerson = Person.Create(personName);
            var fakePerson2 = Person.Create(personName);

            // Assert
            fakePerson.UncommittedEvents.Should().NotBeNull();
            fakePerson.UncommittedEvents.Count().Should().Be(1);
            fakePerson.Id.Should().NotBeEmpty();
            fakePerson.Role.Should().Be(PersonRole.TBD);
            fakePerson.CurrentScore.Should().Be(0);
        }
    }
}
