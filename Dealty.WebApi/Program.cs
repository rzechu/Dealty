using Dealty.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Dealty.WebApi.Controllers;
using Dealty.WebApi.Interfaces;
using Dealty.WebApi.Logger;
using NLog.Web;
using Dealty.Shared.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using static System.Net.WebRequestMethods;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using Dealty.Shared.Interfaces;
using Microsoft.CodeAnalysis.Options;
using Microsoft.OpenApi.Models;

string CORS_POLICY = "CorsPolicy";


IDealtyLogger logger = new DealtyLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var jwtIssuer = configuration["JWT:ValidIssuer"];
var jwtAudience = configuration["JWT:ValidAudience"] ;
byte[] jwtKey = Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"]);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddControllers().AddJsonOptions(x =>
//    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //EF   

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DealtyDBContext>(
    options => {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysqlOptions => mysqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null));
        });

builder.Services.AddSingleton<IDealtyLogger, DealtyLogger>();
builder.Services.AddSingleton<IJWTToken, JWTToken>(o => new JWTToken(jwtIssuer, jwtAudience, jwtKey));
builder.Services.AddSingleton<IUserAuthorization, UserAuthorization>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.SignIn.RequireConfirmedEmail = true;

})
    .AddEntityFrameworkStores<DealtyDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    //options.SaveToken = true;
    //options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
    };
});


builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); //for readonly properties (hidding Ids columns)

    //#region JWT
    //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    //{
    //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    //    Name = "Authorization",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.Http,
    //    Scheme = JwtBearerDefaults.AuthenticationScheme,
    //    BearerFormat = "JWT",
    //});
    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
    // {
    //     {
    //           new OpenApiSecurityScheme
    //             {
    //                 Reference = new OpenApiReference
    //                 {
    //                     Type = ReferenceType.SecurityScheme,
    //                     Id = "Bearer"
    //                 },
    //                 //Scheme = "oauth2",
    //                 //Name = "Bearer",
    //                 //In = ParameterLocation.Header
    //             },
    //             new string[] {}
    //     }
    // });
    //#endregion

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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
