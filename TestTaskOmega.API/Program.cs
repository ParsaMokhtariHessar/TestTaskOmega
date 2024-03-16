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
builder.Services.AddIdentity(builder.Configuration);
//-----------------------Services configurations-------------------------//
builder.Services.AddEntities(builder.Configuration);






builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//-------------------------------------------------------------------------//


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseIdentityDatabaseMigration();
    app.UseProductDatabaseMigration();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
