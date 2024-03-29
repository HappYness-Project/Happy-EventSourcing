﻿//using HP.Domain.Todos.Read;
using HP.Domain.People.Read;
using HP.Domain.Todos.Read;
using HP.Infrastructure.Services;
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
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("postgres"));
        }
        public virtual DbSet<TodoDetails> TodoDetails { get; set; }
        public virtual DbSet<PersonDetails> PersonDetails { get; set; }
        public virtual DbSet<UserAccountStorage> UserAccountStorage { get; set; }
    }
}
