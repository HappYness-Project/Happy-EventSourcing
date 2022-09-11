using HP.Application.Queries;
using HP.Application.Queries.Todos;
using HP.Domain;
using Microsoft.Extensions.Configuration;
using HP.Infrastructure.DbAccess;
using AutoMapper;
using Moq;

namespace HP.IntegrationTest
{
    public class TestBase
    {
        protected IMongoDbContext _mongoDbContext;
        [SetUp]
        public async Task BeforeTestStart()
        {
            // _configuration = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional:false, true).Build();
            // _mongoDbContext = new MongoDbContext(_configuration);
        }
    }
    public class TodoQueryHandlerTest : TestBase
    {
        private readonly Mock<ITodoRepository> _todoRepo;
        private readonly IMapper _mapper;
        private TodoQueryHandlers _queryHandler;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            // var test = TodoQueryHandlers(_mapper, _todoRepo);

            Assert.Pass();
        }
    }
}