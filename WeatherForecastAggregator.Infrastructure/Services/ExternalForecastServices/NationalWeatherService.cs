using System.Text.Json;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.DTOs.NWS;

public class NationalWeatherService : IForecastService
{
   private readonly HttpClient _httpClient;
   private readonly JsonSerializerOptions _jsonSerializerOptions;

   public NationalWeatherService(HttpClient httpClient)
   {
      _httpClient = httpClient;

      _jsonSerializerOptions = new JsonSerializerOptions
      {
         PropertyNameCaseInsensitive = true,
      };
   }

   public async Task<string> GetForecast(Coordinates point)
   {
      var pointResponse = await GetPointData(point);

      var wfo = pointResponse.Properties.Cwa;
      var x = pointResponse.Properties.GridX;
      var y = pointResponse.Properties.GridY;

      var response = await _httpClient.GetAsync($"/gridpoints/{wfo}/{x},{y}/forecast?units=us");

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStreamAsync();

      var dto = await JsonSerializer.DeserializeAsync<ForecastResponseDto>(responseContent, _jsonSerializerOptions);

      var p1 = dto.Properties.Periods[0];
      var p2 = dto.Properties.Periods[1];
      var forecast = $"{p1.Name}: {p1.ShortForecast}, {p1.Temperature}{p1.TemperatureUnit}. {p2.Name}: {p2.ShortForecast}, {p2.Temperature}{p2.TemperatureUnit}. ";
      return forecast;
   }

   private async Task<PointResponseDto> GetPointData(Coordinates point)
   {
      var response = await _httpClient.GetAsync($"/points/{point.Latitude},{point.Longitude}");

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStreamAsync();

      var dto =  await JsonSerializer.DeserializeAsync<PointResponseDto>(responseContent, _jsonSerializerOptions);
      return dto;
   }
}