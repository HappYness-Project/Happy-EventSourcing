using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.DbAccess
{
    internal class MongoDbContext : IMongoDbContext
    {
        public IList<string> Collections()
        {
            throw new NotImplementedException();
        }

        public void CreateCollection<TEntity>(string name = "")
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name = "")
        {
            throw new NotImplementedException();
        }
    }
}
