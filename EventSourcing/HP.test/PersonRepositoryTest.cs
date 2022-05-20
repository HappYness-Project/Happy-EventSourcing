using HP.Domain;
using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;

namespace HP.test
{
    public class PersonRepositoryTest
    {
        IPersonRepository personRepository = null;
        IMongoDbContext dbContext = null;
        // Repository pattern testing
        [SetUp]
        public void Setup()
        {
            var bin_path = Directory.GetCurrentDirectory();
            var check = System.IO.Path.Combine(bin_path, @"..\..\..");
            IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(check)
            .AddJsonFile(@"appsettings.json", false, false)
            .AddEnvironmentVariables()
            .Build();
            dbContext = new MongoDbContext(_configuration);
            personRepository = new PersonRepository(dbContext);
        }

        [Test]
        public void GetAllAsync_ReturnAllPeople()
        {
            var people = personRepository.GetAllAsync();
            Assert.NotNull(people);
         }
    }
}