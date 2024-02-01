using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Infrastructure.DTOs;
using WeatherForecastAggregator.Domain.Models;
using AutoMapper;
using WeatherForecastAggregator.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace WeatherForecastAggregator.Infrastructure.Services.Geocoding
{
   public class BingMapsLocationsService : IGeocodeService
   {
      private readonly HttpClient _httpClient;
      private readonly IMemoryCache _cache;
      private readonly IMapper _mapper;
      private readonly BingMapsOptions _bingMapsOptions;

      public BingMapsLocationsService(HttpClient httpClient, IMemoryCache cache, IMapper mapper, IOptions<BingMapsOptions> bingMapsOptions)
      {
         _httpClient = httpClient;
         _cache = cache;
         _mapper = mapper;
         _bingMapsOptions = bingMapsOptions.Value;
      }

      public async Task<Location?> Geocode(string location)
      {
         var cacheKey = $"geocode_{location}";

         return await _cache.GetOrCreateAsync(cacheKey, async entry =>
         {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return await FetchGeocode(location);
         });
      }

      private async Task<Location> FetchGeocode(string location)
      {
         var uriBuilder = new UriBuilder(_httpClient.BaseAddress)
         {
            Path = "REST/v1/Locations",
            Query = $"q={Uri.EscapeDataString(location)}&maxResults=1&key={_bingMapsOptions.ApiKey}"
         };

         var response = await _httpClient.GetAsync(uriBuilder.Uri);
         response.EnsureSuccessStatusCode();
         var responseContent = await response.Content.ReadAsStreamAsync();

         var options = new JsonSerializerOptions
         {
            PropertyNameCaseInsensitive = true,
         };

         var result = await JsonSerializer.DeserializeAsync<BingMapsResponseDto>(responseContent, options);

         if (result == null)
         {
            throw new Exception("TODO: Use Result pattern");
         }

         return _mapper.Map<Location>(result);
      }
   }
}