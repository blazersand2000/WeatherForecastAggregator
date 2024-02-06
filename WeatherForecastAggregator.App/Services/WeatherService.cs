using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.App.Services
{
   public class WeatherService : IWeatherService
   {
      private readonly ILocationService _locationService;
      private readonly IForecastAggregatorService _forecastAggregatorService;

      public WeatherService(ILocationService locationService, IForecastAggregatorService forecastAggregatorService)
      {
         _locationService = locationService;
         _forecastAggregatorService = forecastAggregatorService;
      }

      public async Task<ForecastsResponse?> GetForecasts(ForecastsRequest request)
      {
         var location = await _locationService.GetLocation(request.Location);
         if (location == null)
         {
            return null;
         }

         var forecasts = await _forecastAggregatorService.GetAggregatedForecast(location.Coordinates);
         if (forecasts == null)
         {
            return null;
         }

         return new ForecastsResponse
         {
            LocationName = location.Name,
            AggregatedForecast = forecasts
         };
      }
   }
}