using HP.Domain.Todos.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.DbAccess
{
    public class HpReadDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public HpReadDbContext(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("postgres"));
        }
        public DbSet<TodoDetails> TodoDetails { get; set; }
    }
}
