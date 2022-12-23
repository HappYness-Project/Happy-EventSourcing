using FluentAssertions;
using HP.Core.Events;
using HP.Domain;
using HP.Domain.Categories;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using HP.test;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace HP.UnitTest.Categories
{


    [TestFixture]
    public class CategoryRepositoryTest : TestBase
    {
        private ICategoryRepository categoryRepository;
        private IEventProducer _eventProducer;
        private IEventStore eventStore;
        public void Setup()
        {
            eventStore = new EventStore(_mongoDbContext, _eventProducer);
            categoryRepository = new CategoryRepository(_mongoDbContext, eventStore);
        }
    }
}