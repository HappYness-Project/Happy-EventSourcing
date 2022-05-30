using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;

namespace HP.test
{
    public class TodoRepositoryTest
    {
        ITodoRepository todoRepository = null;
        IMongoDbContext dbContext = null;
        // Repository pattern testing
        [SetUp]
        public void Setup()
        {

            IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"appsettings.json", false, false)
            .AddEnvironmentVariables()
            .Build();
            dbContext = new MongoDbContext(_configuration);
            todoRepository = new TodoRepository(dbContext);
        }

        [Test]
        public void GetListByKey_Return_Nothing()
        {
            //var check = todoRepository.GetListByTags().Result;
            //Assert.Pass();
        }

        [Test]
        public void Exists_ReturnTrueIfExist()
        {
            var check = todoRepository.Exists(x => x.IsActive);
            Assert.IsTrue(check);
        }
    }
}