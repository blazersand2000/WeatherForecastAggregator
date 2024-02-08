
using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.Interfaces;

namespace WeatherForecastAggregator.App.Services
{
   public class ForecastAggregatorService : IForecastAggregatorService
   {
      private readonly ILocationService _locationService;
      private readonly IEnumerable<IForecastService> _forecastServices;

      public ForecastAggregatorService(ILocationService locationService, IEnumerable<IForecastService> forecastServices)
      {
         _locationService = locationService;
         _forecastServices = forecastServices;
      }

      public async Task<ForecastsResponse?> GetForecasts(ForecastsRequest request)
      {
         var timeZoneInfo = await _locationService.GetTimeZoneInfo(request.Coordinates.Latitude, request.Coordinates.Longitude) ?? TimeZoneInfo.Utc;
         var forecasts = await Task.WhenAll(_forecastServices.Select(s => s.GetForecast(request.Coordinates, timeZoneInfo)));
         if (forecasts == null)
         {
            return null;
         }
         var response = new ForecastsResponse
         {
            Sources = forecasts
         };

         return response;
      }
   }
}
