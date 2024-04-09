using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;


namespace MinimalAPIService.WeatherService
{
    public static class WeatherEndpoints
    {
        public static void AddWeatherServiceEndpoints(this IEndpointRouteBuilder app)
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Get Weather Forecast",
                Description = "Returns information on weather.",
                Tags = new List<OpenApiTag> { new() { Name = "Weather service" } }
            });
        }
        internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
        {
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }

}

