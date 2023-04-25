using Drones.Infrastructure.DataContext;
using Drones.Infrastructure.DataContext.Data;
using Drones.LocalServices;
using Drones.LocalServices.BatteryLevelLogService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddDbContext<AppDBContext>(options => options.UseInMemoryDatabase(databaseName: "InMemory_DB"));  
DbInitializer.Initialize(builder.Services.BuildServiceProvider());

builder.Services.AddScoped<IBatteryLevelLogService,BatteryLevelLogService>();
builder.Services.AddSingleton<PeriodicHostedService>();
builder.Services.AddHostedService(
    provider => provider.GetRequiredService<PeriodicHostedService>());
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
