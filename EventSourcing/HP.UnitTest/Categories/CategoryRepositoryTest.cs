using FluentAssertions;
using HP.Core.Events;
using HP.Domain;
using HP.Domain.Categories;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using HP.Infrastructure.Repository.Write;
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
        public void Setup()
        {
            categoryRepository = new CategoryRepository(_mongoDbContext);
        }
    }
}