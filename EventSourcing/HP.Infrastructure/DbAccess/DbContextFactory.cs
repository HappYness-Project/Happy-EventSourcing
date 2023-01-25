using Microsoft.EntityFrameworkCore;
namespace HP.Infrastructure.DbAccess
{
    public class DbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configDbContext;
        public DbContextFactory(Action<DbContextOptionsBuilder> configDbContext)
        {
            _configDbContext = configDbContext;
        }
        //public DatabaseContext CreateDbContext()
        //{
        //    DbContextOptionsBuilder<DatabaseContext> options = new();
        //    _configDbContext(options);
        //    return new DatabaseContext(options.Options);
        //}
    }
}
