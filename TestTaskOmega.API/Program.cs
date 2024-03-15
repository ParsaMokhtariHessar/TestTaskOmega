using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestTaskOmega.Application;
using TestTaskOmega.Application.Contracts;
using TestTaskOmega.Application.MappingProfiles;
using TestTaskOmega.Application.RepositoryPattern;
using TestTaskOmega.DataAccess;
using TestTaskOmega.Domain;
using TestTaskOmega.Identity;
var builder = WebApplication.CreateBuilder(args);
// ----------------Identity configurations --------------------------------//
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<ApplicationUserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDbContext"));
});

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationUserDbContext>()
    .AddApiEndpoints();
//-----------------------Services configurations-------------------------//
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
builder.Services.AddHttpContextAccessor();
//----------------------AutoMapper------------------------------------//
builder.Services.AddAutoMapper(typeof(MappingProfile));







builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//---------------------------Identity -------------------------------------//
app.MapIdentityApi<ApplicationUser>();
//-------------------------------------------------------------------------//


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
