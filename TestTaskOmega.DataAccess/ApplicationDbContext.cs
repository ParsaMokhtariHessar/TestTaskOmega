using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestTaskOmega.Domain;

namespace TestTaskOmega.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Services> Services { get; set; }
        public DbSet<ServicesHistory> ServicesHistory { get; set; }
        
    }
}
