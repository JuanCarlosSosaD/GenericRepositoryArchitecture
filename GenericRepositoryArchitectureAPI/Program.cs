using Everyware.GRInfrastructure;
using GenericRepositoryArchitectureAPI.Configurations;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices(builder.Configuration);
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


var corsPolicyOptions = app.Configuration.GetSection(CorsPolicyOptions.CorsPolicy).Get<CorsPolicyOptions>();

if (corsPolicyOptions is { })
{
   //Asignar políticas desde el objeto de políticas
}

app.Run();
