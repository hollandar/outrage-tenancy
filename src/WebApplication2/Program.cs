using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tenancy;
using Tenancy.Data;
using Tenancy.Providers;
using WebApplication2.Extensions;
using WebApplication2.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHttpHandler, WeatherForecastHandler>();
builder.Services.AddTenancy(dbOptions =>
{
    dbOptions.UseNpgsql(builder.Configuration.GetConnectionString("Tenant"), o => o.MigrationsAssembly("Tenancy.Postgres"));
}, options =>
{
    builder.Configuration.GetSection("TenantOptions").Bind(options);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedRedisCache(options =>
{
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions() { 
        Password = "eBsCvNwBOfT1yDl7" 
    };
    options.ConfigurationOptions.EndPoints.Add("localhost:6379");
    options.InstanceName = "";
});
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "app_session";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSession();

app.MapGetHandler<WeatherForecastRequest>("weatherforecast").WithName("GetWeatherForecast").WithOpenApi();

app.Run();


