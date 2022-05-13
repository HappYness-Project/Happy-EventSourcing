using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //   _dbName = configuration["Mongo:Read:Database"];
            //   _connStr = configuration["Mongo:Read:Connection"];
            _connStr = configuration["ConnectionStrings:mongo"];
            _dbName = configuration["ConnectionStrings:dbname"];

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_connStr));
            _mongoClient = new MongoClient(settings);
            _mongoDb = _mongoClient.GetDatabase(_dbName);
        }

        public IList<string> Collections()
        {
            var collections = _mongoDb.ListCollections().ToList();
            return collections.Select(c => c["name"].ToString()).ToList();
        }

        public void CreateCollection<TEntity>(string name = "")
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(TEntity).Name + "s";

            _mongoDb.CreateCollection(name);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name = "")
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(TEntity).Name + "s";

            return _mongoDb.GetCollection<TEntity>(name);
        }
    }
}
