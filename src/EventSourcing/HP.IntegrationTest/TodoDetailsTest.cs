using AutoMapper;
using FluentAssertions;
using HP.Core.Common;
using HP.Domain;
using HP.Domain.Todos.Read;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;


namespace HP.IntegrationTest
{
    [Category("Integration")]
    public class TodoDetailsTest
    {

        protected IConfiguration _configuration;
        private HpReadDbContext _dbContext;
        private IBaseRepository<TodoDetails> _todoDetailsRepository;
        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional: false, true).Build();
            _dbContext = new HpReadDbContext(_configuration);
            _todoDetailsRepository = new BaseRepository<TodoDetails>(_dbContext);
        }
        [Test]
        public async Task NpgSqlDbConnection()
        {
            var check = await _todoDetailsRepository.GetAllAsync();
            
            check.Should().NotBeNull();
        }

        [Test]
        public async Task TodoDetailsRepo_CreateTodo_ReturnSuccess()
        {
            string[] Tags = new string[] { "Testing Application", "TodoDetails Tags" };
            TodoDetails todoDetails = new TodoDetails(Guid.NewGuid())
            {
                Title = "Test",
                Description = "Test Desc",
                TodoType = "Normal",
                PersonId = Guid.NewGuid(),
                Tags = Tags,
                IsActive = true,
                IsDone = false,
                Score = 0,
                TodoStatus = "Pending",
            };

            //var check = await _todoDetailsRepository.Get
            //Assert.IsNotNull(check);
        }
    }
}
