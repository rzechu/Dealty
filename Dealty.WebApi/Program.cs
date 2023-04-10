using Dealty.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Dealty.WebApi.Controllers;
using Dealty.WebApi.Interfaces;
using Dealty.WebApi.Logger;
using NLog.Web;
using Dealty.Shared.Data;

string CORS_POLICY = "CorsPolicy";

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
    options.EnableAnnotations(); //for readonly properties (hidding Ids columns)
});


//builder.Services.AddControllers().AddJsonOptions(x =>
//    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //EF   

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DealtyDBContext>(
    options => {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysqlOptions => mysqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null));
        });


builder.Services.AddSingleton<IDealtyLogger, DealtyLogger>();
builder.Services.AddScoped<ICategoryRepositoryAsync, CategoryRepository>();
builder.Services.AddScoped<IPromotionRepositoryAsync, PromotionRepository>();

builder.Services.AddCors(policies =>
{
    policies.AddPolicy(CORS_POLICY, opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(CORS_POLICY);

app.UseAuthorization();
app.MapControllers();

app.Run();