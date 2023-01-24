using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.DbAccess
{
    public class PostgreDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public PostgreDbContext(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        protected override void OnCo
    }
}
