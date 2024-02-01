using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.App.Services
{
   public class ForecastService : IForecastService
   {
      private readonly IGeocodeService _geocodeService;

      public ForecastService(IGeocodeService geocodeService)
      {
         _geocodeService = geocodeService;
      }

      public async Task<AggregatedForecast> GetForecasts(ForecastsRequest request)
      {
         var location = await _geocodeService.Geocode(request.Location);
         var name = (location?.Name ?? string.Empty) + ": " + (location?.Coordinates.Latitude) + ", " + (location?.Coordinates.Longitude);
         return new AggregatedForecast
         {
            Sources = new[]
            {
               new ForecastSource
               {
                  Name = name,
                  CurrentTemperatureF = 75
               },
               new ForecastSource
               {
                  Name = "WeatherSource2",
                  CurrentTemperatureF = 78
               }
            }
         };
      }
   }
}