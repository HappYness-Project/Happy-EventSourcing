using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;

namespace HP.test
{
    [TestFixture]
    public class TodoRepositoryTest : TestBase
    {
        private ITodoRepository todoRepository;
        [SetUp]
        public void Setup()
        {
            IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"appsettings.json", false, false)
            .AddEnvironmentVariables()
            .Build();
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
            var todo = Todo.CreateTodo("userId7303", "Create Todo through the UnitTest", "General", null);
            var todoObj = todoRepository.CreateAsync(todo)?.Result;
            Assert.NotNull(todoObj);
        }
    }
}