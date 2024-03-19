using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTaskOmega.Application.Contracts;
using TestTaskOmega.Application.RepositoryPattern;
using TestTaskOmega.Application.ServiceRepository;
using TestTaskOmega.DataAccess;

namespace TestTaskOmega.Application
{
    public static class IdentityConfigurationExtension
    {
        public static IServiceCollection AddEntities(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"))
            );

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IServicesRepository, ServicesRepository>();
            
            return services;
        }
    }
}
