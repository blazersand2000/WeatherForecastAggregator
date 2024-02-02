using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.App.Services
{
   public class WeatherAggregatorService : IWeatherAggregatorService
   {
      private readonly IGeocodeService _geocodeService;
      private readonly IForecastService _forecastService;

      public WeatherAggregatorService(IGeocodeService geocodeService, IForecastService forecastService)
      {
         _geocodeService = geocodeService;
         _forecastService = forecastService;
      }

      public async Task<AggregatedForecast> GetForecasts(ForecastsRequest request)
      {
         var location = await _geocodeService.Geocode(request.Location);
         var forecast = await _forecastService.GetForecast(location.Coordinates);
         return new AggregatedForecast
         {
            Location = location.Name,
            Sources = new[]
            {
               new ForecastSource
               {
                  Name = $"National Weather Service - {forecast}",
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