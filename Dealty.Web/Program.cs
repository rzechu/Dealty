using Dealty.Shared.Data;
using Dealty.Shared.Services;
using Dealty.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);


string BASE_ADRESS = builder.Configuration.GetSection("webapiUrl")?.Value ?? throw new ArgumentNullException("webapiUrl");
//builder.Services.AddScoped(sp =>
//{
//    var client = new HttpClient();
//    client.BaseAddress = new Uri(BASE_ADRESS);
//    return client;
//});

builder.Services.AddHttpClient("DealtyHttpClient", options =>
{
    options.BaseAddress = new Uri(BASE_ADRESS);
    options.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddSingleton<PromotionStateContainer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
