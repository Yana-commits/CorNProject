
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Confgurations;
using REST_API.Repositories;
using REST_API.Repositories.Interfaces;
using REST_API.Services;
using REST_API.Services.Interfaces;

//var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IProducedOperationsService, ProducedOperatonsServce>();
builder.Services.AddTransient<IProducedOperationsRepository, ProducedOperationsRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ApplcationDBContext>(options =>
//options.UseSqlServer(configuration["ConnectionString"]));
builder.Services.AddDbContext<ApplcationDBContext>(options =>
options.UseSqlServer(
 builder.Configuration.GetConnectionString("DefaultConnection")
    ));

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

//IConfiguration GetConfiguration()
//{
//    var builder = new ConfigurationBuilder()
//        .SetBasePath(Directory.GetCurrentDirectory())
//        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//        .AddEnvironmentVariables();

//    return builder.Build();
//}
