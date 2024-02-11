using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.DTOs.OpenWeatherMap;
using WeatherForecastAggregator.Infrastructure.Interfaces;
using WeatherForecastAggregator.Infrastructure.Options;

public class OpenWeatherMapApi : IOpenWeatherMapDataProvider
{
   private readonly HttpClient _httpClient;
   private readonly OpenWeatherMapOptions _openWeatherMapOptions;
   private readonly JsonSerializerOptions _jsonSerializerOptions;

   public OpenWeatherMapApi(HttpClient httpClient, IOptions<OpenWeatherMapOptions> openWeatherMapOptions)
   {
      _httpClient = httpClient;
      _openWeatherMapOptions = openWeatherMapOptions.Value;

      _jsonSerializerOptions = new JsonSerializerOptions
      {
         PropertyNameCaseInsensitive = true,
      };
   }

   public async Task<ForecastResponseDto> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo)
   {
      var response = await _httpClient.GetAsync($"/data/2.5/forecast?lat={point.Latitude}&lon={point.Longitude}&appid={_openWeatherMapOptions.ApiKey}&units=imperial");

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStreamAsync();

      var dto = await JsonSerializer.DeserializeAsync<ForecastResponseDto>(responseContent, _jsonSerializerOptions);

      return dto;
   }
}