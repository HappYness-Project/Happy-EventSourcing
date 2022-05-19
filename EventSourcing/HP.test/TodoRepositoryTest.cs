using HP.Domain.Todos;
using NUnit.Framework;

namespace HP.test
{
    public class Tests
    {
        ITodoRepository todoRepository = null;

        // Repository pattern testing
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}