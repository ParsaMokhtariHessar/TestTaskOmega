using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System;
using TestTaskOmega.Identity;
var builder = WebApplication.CreateBuilder(args);
// ----------------Identity configurations --------------------------------//
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<IdentityDbContext>(
    options => options.UseSqlServer("TestTaskOmegaDatabase"));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityDbContext>();
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddApiEndpoints();
// -----------------------------------------------------------------------//

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
