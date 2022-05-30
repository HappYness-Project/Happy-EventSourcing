using MongoDB.Driver;

namespace HP.Infrastructure.DbAccess
{
    public interface IMongoDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name = "");
        void CreateCollection<TEntity>(string name = "");
        IList<string> Collections();
        IClientSessionHandle Session { get; }
    }
}