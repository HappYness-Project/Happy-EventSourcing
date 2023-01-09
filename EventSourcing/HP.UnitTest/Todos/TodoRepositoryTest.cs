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
        [SetUp]
        public void Setup()
        {
            todoRepository = new TodoRepository(_mongoDbContext);
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
            var expectedType = "Normal";

            // Act
            var todo = TodoFactory.Create(expectedUserName, expectedTitle, expectedType, expectedDesc);
            var todoObj = todoRepository.CreateAsync(todo)?.Result;

            // Assert
            todoObj.PersonName.Should().Be(expectedUserName);
            todoObj.Title.Should().Be(expectedTitle);
            todoObj.Description.Should().Be(expectedDesc);
        }
        [Test]
        public void Delete_ReturnFalseIfExist()
        {

        }
        [Test]
        public void Find_ReturnObjectIfExist()
        {

            var todo = TodoFactory.Create();

            todoRepository.Find(x => x.AddDomainEvent== null);

        }



    }
}