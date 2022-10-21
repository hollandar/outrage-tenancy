using Outrage.Tenancy;

namespace WebApplication2.Handlers
{
    public struct WeatherForecastRequest
    {
        public string City { get; set; }
    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public sealed class WeatherForecastHandler : IHttpGetHandler<WeatherForecastRequest>
    {
        private readonly ITenancyService tenacyService;

        public WeatherForecastHandler(ITenancyService tenacyService) {
            this.tenacyService = tenacyService;
        }
        
        public async Task<IResult> GetAsync(HttpContext context, WeatherForecastRequest request)
        {
            var tenant = await this.tenacyService.GetTenantAsync();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();

            return Results.Ok(new { tenantId = tenant.Definition.TenantId.AsString, forecast = forecast });
        }
    }
}
