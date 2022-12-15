using FluentAssertions;
using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.Repository;
using HP.test;
using HP.Core.Events;
using NUnit.Framework;

namespace HP.UnitTest.Todos
{


    [TestFixture]
    public class TodoRepositoryTest : TestBase
    {
        private ITodoRepository todoRepository;
        private IEventStore eventStore = null;
        [SetUp]
        public void Setup()
        {
            eventStore = new EventStore(_configuration, _mongoDbContext);
            todoRepository = new TodoRepository(_mongoDbContext, eventStore);
            // Seed Data Insertion?
        }
        [Test]
        public void GetListByUserId_Return_Nothing()
        {
            var check = todoRepository.GetListByUserId("userId7303");
            Assert.That(check, Is.Not.Null);
        }
        [Test]
        public void Exists_ReturnTrueIfExist()
        {
            var check = todoRepository.Exists(x => x.IsActive);
            Assert.IsTrue(check);
        }
        [Test]
        public void CreateNewTodo_From_Repository()
        {
            // Arrange
            var expectedUserName = "TestUser123";
            var expectedTitle = "Creating Todo";
            var expectedDesc = "Description Testing";

            // Act
            var todo = TodoFactory.Create(expectedUserName, expectedTitle, expectedDesc, true);
            var todoObj = todoRepository.CreateAsync(todo)?.Result;

            // Assert
            todoObj.UserId.Should().Be(expectedUserName);
            todoObj.Title.Should().Be(expectedTitle);
            todoObj.Description.Should().Be(expectedDesc);
        }



    }
}