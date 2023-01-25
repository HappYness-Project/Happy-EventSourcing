using FluentAssertions;
using HP.Domain.Todos.Write;
using HP.Infrastructure.Repository.Write;
using HP.test;
using NUnit.Framework;

namespace HP.UnitTest.Todos
{


    [TestFixture]
    public class TodoRepositoryTest : TestBase
    {
        private ITodoAggregateRepository todoRepository;
        [SetUp]
        public void Setup()
        {
            todoRepository = new TodoAggregateRepository(_mongoDbContext);
        }
        [Test]
        public void GetListByUserId_Return_Nothing()
        {
            var userId = "userId7303";

            //Act
            var check = todoRepository.GetListByPersonName(userId);
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
            todoObj.PersonId.Should().Be(expectedUserName);
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

        }



    }
}