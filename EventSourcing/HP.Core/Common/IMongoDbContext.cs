using MongoDB.Driver;

namespace HP.Core.Common
{
    public interface IMongoDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name = "");
        void CreateCollection<TEntity>(string name = "");
    }
}