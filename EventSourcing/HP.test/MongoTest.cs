using HP.Domain;
using HP.Domain.Todos;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace HP.test
{
    public class MongoTest
    {
        private IBaseRepository<Todo> _repository;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            var dbContextMock = new Mock<ProductsDbContext>();
            var dbSetMock = new Mock<DbSet<Todo>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Todo()));
            dbContextMock.Setup(s => s.Set<Todo>()).Returns(dbSetMock.Object);

            //Execute method of SUT (ProductsRepository)  
            var productRepository = new TodoRepository(dbContextMock.Object);
            var product = productRepository.GetByIdAsync(Guid.NewGuid()).Result;

            //Assert  
            Assert.NotNull(product);
            Assert.IsAssignableFrom<Product>(product);
            Assert.Pass();
        }
    }
}
