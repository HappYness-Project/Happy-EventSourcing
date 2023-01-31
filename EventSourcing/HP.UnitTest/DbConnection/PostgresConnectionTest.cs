using AutoMapper;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository.Read;
using HP.Infrastructure.Repository.Write;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.UnitTest.DbConnection
{
    public class PostgresConnectionTest
    {

        protected IConfiguration _configuration;
        private HpReadDbContext _dbContext;
        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional: false, true).Build();
            //_dbContext = new HpReadDbContext(_configuration);
        }
        [Test]
        public void NpgSqlDbConnection()
        {

        }
    }
}
