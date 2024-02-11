using System.Text.Json;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.DTOs.NWS;
using WeatherForecastAggregator.Infrastructure.Interfaces;

public class NationalWeatherServiceApi : INationalWeatherServiceDataProvider
{
   private readonly HttpClient _httpClient;
   private readonly JsonSerializerOptions _jsonSerializerOptions;

   public NationalWeatherServiceApi(HttpClient httpClient)
   {
      _httpClient = httpClient;

      _jsonSerializerOptions = new JsonSerializerOptions
      {
         PropertyNameCaseInsensitive = true,
      };
   }

   public async Task<ForecastResponseHourlyDto> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo)
   {
      var pointResponse = await GetPointData(point);

      var wfo = pointResponse.Properties.Cwa;
      var x = pointResponse.Properties.GridX;
      var y = pointResponse.Properties.GridY;

      return await GetHourlyForecast(wfo, x, y, timeZoneInfo);
   }

   private async Task<ForecastResponseHourlyDto> GetHourlyForecast(string wfo, int x, int y, TimeZoneInfo timeZoneInfo)
   {
      var response = await _httpClient.GetAsync($"/gridpoints/{wfo}/{x},{y}/forecast/hourly?units=us");

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStreamAsync();

      var dto = await JsonSerializer.DeserializeAsync<ForecastResponseHourlyDto>(responseContent, _jsonSerializerOptions);

      return dto;
   }

   private async Task<ForecastSource> GetTwicePerDayForecast(string wfo, int x, int y)
   {
      var response = await _httpClient.GetAsync($"/gridpoints/{wfo}/{x},{y}/forecast?units=us");

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStreamAsync();

      var dto = await JsonSerializer.DeserializeAsync<ForecastResponseDto>(responseContent, _jsonSerializerOptions);

      var p1 = dto.Properties.Periods[0];
      var p2 = dto.Properties.Periods[1];
      var forecast = new ForecastSource
      {
         Name = "National Weather Service",
         Attribution = new AttributionNode
         {
            Text = "NWS Attribution Text", // replace with actual text
            Url = "https://example.com/attribution", // replace with actual URL
            LogoUrl = "https://example.com/logo.png" // replace with actual URL or null if not applicable
         }
      };

      return forecast;
   }

   private async Task<PointResponseDto> GetPointData(Coordinates point)
   {
      var response = await _httpClient.GetAsync($"/points/{point.Latitude},{point.Longitude}");

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStreamAsync();

      var dto = await JsonSerializer.DeserializeAsync<PointResponseDto>(responseContent, _jsonSerializerOptions);
      return dto;
   }
}