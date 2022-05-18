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

            //
            var settings = MongoClientSettings.FromConnectionString("mongodb://hyunbin7303:asdf1234@localhost:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("test");
        }

        [Test]
        public void DatabaseIsConnected_to_Local()
        {

        }


        [Test]
        public void Test1()
        {
            //mongoDbContext.GetCollection("");
        }
    }
}
