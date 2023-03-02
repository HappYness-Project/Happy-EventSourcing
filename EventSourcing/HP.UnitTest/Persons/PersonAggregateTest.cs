using FluentAssertions;
using HP.Domain;
using NUnit.Framework;
using System.Linq;

namespace HP.UnitTest.Persons
{
    public class PersonAggregateTest
    {
        [Test]
        public void CreatePerson_Should_Success()
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
            fakePerson.Type.Should().Be("Normal");
            fakePerson.CurrentScore.Should().Be(0);
        }


        [Test]
        public void CreatePerson_And_Set_Role_To_Admin_Success()
        {
            // Arrange 
            string personName = "hyunbin7303";
            string setupRole = PersonRole.Admin.ToString();
            // Act
            var fakePerson = Person.Create(personName);
            fakePerson.UpdateRole(setupRole);

            // Assert
            fakePerson.UncommittedEvents.Should().NotBeNull();
            fakePerson.UncommittedEvents.Count().Should().Be(2);
            fakePerson.Id.Should().NotBeEmpty();
            fakePerson.Role.Should().Be(PersonRole.Admin);
        }
        [Test]
        public void CreatePerson_And_SetGroupId_Success()
        {
            // Arrange 
            string personName = "hyunbin7303";
            int groupId = 1;
            // Act
            var fakePerson = Person.Create(personName);
            fakePerson.UpdateGroupId(groupId);

            // Assert
            fakePerson.UncommittedEvents.Should().NotBeNull();
            fakePerson.UncommittedEvents.Count().Should().Be(2);
            fakePerson.Id.Should().NotBeEmpty();
            fakePerson.GroupId.Should().Be(groupId);
        }
    }
}
