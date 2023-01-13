using HP.Core.Events;
using HP.Domain;
using HP.Domain.Categories;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore.Metadata;
using MongoDB.Driver;
namespace HP.Infrastructure.Repository
{
    public class CategoryRepository : BaseAggregateRepository<Category>, ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;
        public CategoryRepository(IMongoDbContext dbContext) : base(dbContext)
        {
            _categories = dbContext.GetCollection<Category>();
        }
        public Task<IEnumerable<Category>> GetCategoriesByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
