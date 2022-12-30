using HP.Core.Events;
using HP.Domain;
using HP.Domain.Categories;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore.Metadata;
using MongoDB.Driver;
namespace HP.Infrastructure.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly IEventStore _eventStore;
        public CategoryRepository(IMongoDbContext dbContext, IEventStore eventStore) : base(dbContext)
        {
            _categories = dbContext.GetCollection<Category>();
            _eventStore = eventStore;
        }
        public Task<IEnumerable<Category>> GetCategoriesByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
