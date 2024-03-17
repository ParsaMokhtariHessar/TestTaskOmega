using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestTaskOmega.Identity.IdentityModels;
using TestTaskOmega.Identity.IdentityData;
using TestTaskOmega.Identity.IdentityServices.UserManagmentService;
using TestTaskOmega.Identity.IdentityServices.AuthenticationService;

namespace TestTaskOmega.Identity
{
    public static class IdentityBuilderExtension
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<ApplicationUserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityDbContext")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationUserDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserManagementService, UserManagementService>();

            string? issuer = configuration["JwtSettings:Issuer"];
            string? audience = configuration["JwtSettings:Audience"];
            string? jwtKey = configuration["JwtSettings:Key"];

            if (issuer == null || audience == null || jwtKey == null)
            {
                throw new InvalidOperationException("JwtSettings: Issuer, Audience, or Key is null");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            return services;
        }
    }
}

