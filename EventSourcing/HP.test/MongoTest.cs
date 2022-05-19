using HP.Domain;
using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HP.test
{
    public class MongoTest
    {
        private IConfiguration _configuration;
        private IMongoDbContext mongoDbContext;

        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
            mongoDbContext = new MongoDbContext(_configuration);


        }
        [Test]
        public void MongoDbContext_Collections_Exist()
        {
            Assert.IsNotNull(mongoDbContext.Collections());
        }
        [Test]
        public void DbCotextReturnCollectionTodo()
        {
            var check = mongoDbContext.GetCollection<Todo>("todo");
            Assert.IsNotNull(check);
        }



    }
}
