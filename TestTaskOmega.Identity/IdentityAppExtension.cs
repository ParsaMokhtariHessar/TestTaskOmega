using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskOmega.Identity.IdentityData;

namespace TestTaskOmega.Identity
{
    public static class IdentityDatabaseMigrationExtensions
    {
        public static IApplicationBuilder UseIdentityDatabaseMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationUserDbContext>();
                dbContext.Database.Migrate();
                // You can also seed data here if needed
            }
            return app;
        }
    }
}
