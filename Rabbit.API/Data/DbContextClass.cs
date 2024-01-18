using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rabbit.API.Models;

namespace Rabbit.API.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration) {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet < Product > Products {
            get;
            set;
        }
    }
}