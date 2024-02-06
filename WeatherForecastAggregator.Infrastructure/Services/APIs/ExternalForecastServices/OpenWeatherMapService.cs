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

   public async Task<ForecastSource> GetForecast(Coordinates point)
   {
      var response = await _httpClient.GetAsync($"/data/2.5/forecast?lat={point.Latitude}&lon={point.Longitude}&appid={_openWeatherMapOptions.ApiKey}&units=imperial");

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStreamAsync();

      var dto = await JsonSerializer.DeserializeAsync<ForecastResponseDto>(responseContent, _jsonSerializerOptions);
      var p1 = dto.List[0];
      var temp = p1.Main.Temp;
      var time = DateTimeOffset.FromUnixTimeSeconds(p1.Dt).DateTime;
      var conditions = p1.Weather[0].Description;
      var forecast = new ForecastSource
      {
         Name = "OpenWeatherMap",
         CurrentTemperatureF = temp, // assuming `temp` is a float representing the current temperature
         Attribution = new AttributionNode
         {
            Text = "Attribution Text", // replace with actual text
            Url = "https://example.com/attribution", // replace with actual URL
            LogoUrl = "https://example.com/logo.png" // replace with actual URL or null if not applicable
         }
      };

      return forecast;
   }
}