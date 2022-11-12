using HP.Domain;
using HP.Domain.Categories;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;
        public CategoryRepository(IMongoDbContext dbContext, IEventStore eventStore)
        {
            _categories = dbContext.GetCollection<Category>();
        }
        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategoriesByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
