using Dataverse.Web.API.Models;
using Dataverse.Http.Connector.Core;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Connection;
using Dataverse.Http.Connector.Core.Extensions.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

// Configure Dataverse HTTP Connector Core.
builder.Services.AddDataverseContext<DataverseContext>(config =>
{
    config.SetDefaultConnection
    (
        builder.Configuration.GetSection("Dataverse").Get<DataverseConnection>()
    );
    config.AddEntitiesFromAssembly(typeof(Employees));
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
