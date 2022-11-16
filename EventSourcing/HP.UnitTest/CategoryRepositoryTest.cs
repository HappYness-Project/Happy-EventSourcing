using FluentAssertions;
using HP.Domain;
using HP.Domain.Categories;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace HP.test
{


    [TestFixture]
    public class CategoryRepositoryTest : TestBase
    {
        private ICategoryRepository categoryRepository;
        private IEventStore eventStore;
        public void Setup()
        {
            eventStore = new EventStore(_configuration, _mongoDbContext);
            categoryRepository = new CategoryRepository(_mongoDbContext, eventStore);
        }
        [Test]
        public async Task GetCategories_ReturnCateogryObjects()
        {
            var check = categoryRepository.GetCategories();
            Assert.That(check, Is.Not.Null);    
        }
        [Test]
        public void Exists_ReturnTrueIfExist()
        {
            Category category = new Category() { Id = 1, IsDone = false, };
            CategoryItem categoryItem = new CategoryItem() { };
        }



    }
}