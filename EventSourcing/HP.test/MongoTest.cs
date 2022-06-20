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
    public class MongoTest : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void MongoDbContext_Collections_Exist()
        {
            Assert.IsNotNull(_mongoDbContext.Collections());
        }
        [Test]
        public void DbCotextReturnCollectionTodo()
        {
            var check = _mongoDbContext.GetCollection<Todo>("todo");
            Assert.IsNotNull(check);
        }



    }
}
