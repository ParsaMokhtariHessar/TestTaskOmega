using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestTaskOmega.Identity.IdentityModels;
using TestTaskOmega.Identity.IdentityData;

namespace TestTaskOmega.Identity
{
    public static class IdentityBuilderExtension
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            services.AddAuthorizationBuilder();

            services.AddDbContext<ApplicationUserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityDbContext"));
            });

            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationUserDbContext>();
                

            return services;
        }
    }
}
