using HP.Core.Common;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace HP.Infrastructure.DbAccess
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly string _dbName;
        private readonly string _connStr;
        private readonly IMongoDatabase _mongoDb;
        private readonly IMongoClient _mongoClient;
        public MongoDbContext(IConfiguration configuration)
        {
            _connStr = configuration["ConnectionStrings:mongo"];
            _dbName = configuration["ConnectionStrings:dbname"];
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_connStr));
            _mongoClient = new MongoClient(settings);
            _mongoDb = _mongoClient.GetDatabase(_dbName);
        }

        public void CreateCollection<TEntity>(string name = "")
        {
            throw new NotImplementedException();
        }
        public IMongoCollection<TEntity> GetCollection<TEntity>(string name = "")
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(TEntity).Name + "s";

            return _mongoDb.GetCollection<TEntity>(name);
        }
    }
}
