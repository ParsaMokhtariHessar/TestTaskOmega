using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TestTaskOmega.Application;
using TestTaskOmega.DataAccess;
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
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authorization",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
