using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;
using WeatherForecastAggregator.Infrastructure.Options;
using Microsoft.Extensions.Options;
using WeatherForecastAggregator.Infrastructure.DTOs.BingMaps;

namespace WeatherForecastAggregator.Infrastructure.Services.APIs.Geocoding
{
   public class BingMapsLocationsService : IGeocodeService
   {
      private readonly HttpClient _httpClient;
      private readonly JsonSerializerOptions _jsonSerializerOptions;
      private readonly IMemoryCache _cache;
      private readonly IMapper _mapper;
      private readonly BingMapsOptions _bingMapsOptions;

      public BingMapsLocationsService(HttpClient httpClient, IMemoryCache cache, IMapper mapper, IOptions<BingMapsOptions> bingMapsOptions)
      {
         _httpClient = httpClient;
         _cache = cache;
         _mapper = mapper;
         _bingMapsOptions = bingMapsOptions.Value;

         _jsonSerializerOptions = new JsonSerializerOptions
         {
            PropertyNameCaseInsensitive = true,
         };
      }

      public async Task<BingMapsResponseDto?> Geocode(string location)
      {
         var cacheKey = $"geocode_{location}";

         return await _cache.GetOrCreateAsync(cacheKey, async entry =>
         {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return await FetchGeocode(location);
         });
      }

      private async Task<BingMapsResponseDto?> FetchGeocode(string location)
      {
         var uriBuilder = new UriBuilder(_httpClient.BaseAddress)
         {
            Path = "REST/v1/Locations",
            Query = $"q={Uri.EscapeDataString(location)}&maxResults=1&key={_bingMapsOptions.ApiKey}"
         };

         var response = await _httpClient.GetAsync(uriBuilder.Uri);
         response.EnsureSuccessStatusCode();
         var content = await response.Content.ReadAsStreamAsync();

         var result = await JsonSerializer.DeserializeAsync<BingMapsResponseDto>(content, _jsonSerializerOptions);

         if (result == null)
         {
            throw new Exception("TODO: Use Result pattern");
         }

         return result;
      }

      public async Task<TimeZoneResponseDto?> GetTimeZoneInfo(double latitude, double longitude)
      {
         var cacheKey = $"TimeZoneInfo-{latitude}-{longitude}";

         return await _cache.GetOrCreateAsync(cacheKey, async entry =>
         {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return await FetchTimeZoneInfo(latitude, longitude);
         });
      }

      private async Task<TimeZoneResponseDto?> FetchTimeZoneInfo(double latitude, double longitude)
      {
         var response = await _httpClient.GetAsync($"REST/v1/timezone/{latitude},{longitude}?key={_bingMapsOptions.ApiKey}");
         response.EnsureSuccessStatusCode();
         var content = await response.Content.ReadAsStreamAsync();
         var result = await JsonSerializer.DeserializeAsync<TimeZoneResponseDto>(content, _jsonSerializerOptions);

         if (result == null)
         {
            throw new Exception("TODO: Use Result pattern");
         }

         return result;
      }
   }
}