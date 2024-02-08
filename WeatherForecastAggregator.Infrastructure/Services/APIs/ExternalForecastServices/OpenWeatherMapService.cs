using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.DTOs.OpenWeatherMap;
using WeatherForecastAggregator.Infrastructure.Interfaces;
using WeatherForecastAggregator.Infrastructure.Options;

public class OpenWeatherMapService : IForecastService
{
   private readonly HttpClient _httpClient;
   private readonly OpenWeatherMapOptions _openWeatherMapOptions;
   private readonly JsonSerializerOptions _jsonSerializerOptions;

   public OpenWeatherMapService(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherMapOptions> openWeatherMapOptions)
   {
      _httpClient = httpClientFactory.CreateClient(nameof(OpenWeatherMapService));
      _openWeatherMapOptions = openWeatherMapOptions.Value;

      _jsonSerializerOptions = new JsonSerializerOptions
      {
         PropertyNameCaseInsensitive = true,
      };
   }

   public async Task<ForecastSource> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo)
   {
      var response = await _httpClient.GetAsync($"/data/2.5/forecast?lat={point.Latitude}&lon={point.Longitude}&appid={_openWeatherMapOptions.ApiKey}&units=imperial");

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStreamAsync();

      var dto = await JsonSerializer.DeserializeAsync<ForecastResponseDto>(responseContent, _jsonSerializerOptions);

      var periodsGroupedByDay = dto.List
         .GroupBy(p => TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.FromUnixTimeSeconds(p.Dt).UtcDateTime, timeZoneInfo).Date);

      var dailyForecasts = periodsGroupedByDay.Select(g => new DailyForecast
      {
         Date = g.Key.ToString("yyyy-MM-dd"),
         HighTemperatureF = g.Max(p => p.Main.Temp),
         LowTemperatureF = g.Min(p => p.Main.Temp),
         ShortForecast = string.Join(", ", g.SelectMany(p => p.Weather).Select(w => w.Description).Distinct()),
         ProbabilityOfPrecipitation = g.Max(p => p.Pop),
         WindSpeed = g.Max(p => (int)Math.Round(p.Wind.Speed))
      });

      var forecast = new ForecastSource
      {
         Name = "OpenWeatherMap",
         Attribution = new AttributionNode
         {
            Text = "Weather data provided by OpenWeather",
            Url = "https://openweathermap.org/",
            LogoUrl = "/OpenWeather-Master-Logo RGB.png"
         },
         DailyForecasts = dailyForecasts.ToList()
      };

      return forecast;
   }
}