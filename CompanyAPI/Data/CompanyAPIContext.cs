using CompanyAPI.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Data
{
    public class CompanyAPIContext : DbContext
    {
        public CompanyAPIContext(DbContextOptions<CompanyAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
