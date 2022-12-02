using HP.Core.Events;
using HP.Domain;
using HP.Domain.Categories;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
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
        public Task CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
