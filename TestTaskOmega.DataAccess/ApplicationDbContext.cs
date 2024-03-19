using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestTaskOmega.Domain;

namespace TestTaskOmega.DataAccess
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Services> Services { get; set; }
        //
    }
}
