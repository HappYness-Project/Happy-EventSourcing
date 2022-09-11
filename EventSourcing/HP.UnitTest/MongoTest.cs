using HP.Domain;
using NUnit.Framework;

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
