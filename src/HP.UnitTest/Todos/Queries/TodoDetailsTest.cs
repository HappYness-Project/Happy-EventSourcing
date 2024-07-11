using AutoMapper;
using FluentAssertions;
using HP.Core.Common;
using HP.Domain;
using HP.Domain.Todos.Read;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Threading.Tasks;


namespace HP.IntegrationTest
{ 
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
        public async Task TodoDetailsRepo_GetByIdAsync_ReturnSuccess()
        {
            var check = await _todoDetailsRepository.GetByIdAsync(Guid.NewGuid());

            check.Should().BeNull();
        }
    }
}
