using HP.Infrastructure.DbAccess;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.test
{
    public class TestBase
    {
        protected IConfiguration _configuration;
        protected IMongoDbContext _mongoDbContext;
        [SetUp]
        public async Task BeforeTestStart()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional:false, true).Build();
            _mongoDbContext = new MongoDbContext(_configuration);
        }
    }
}
