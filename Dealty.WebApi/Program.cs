using Dealty.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Dealty.WebApi.Controllers;
using Dealty.WebApi.Interfaces;
using Microsoft.Extensions.Options;
using Dealty.WebApi.Logger;
using NLog.Web;
using NLog;


IDealtyLogger logger = new DealtyLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DealtyDBContext>(
    options => {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysqlOptions => mysqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null));
        });


builder.Services.AddSingleton<IDealtyLogger, DealtyLogger>();
builder.Services.AddScoped<ICategoryRepositoryAsync, CategoryRepository>();

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