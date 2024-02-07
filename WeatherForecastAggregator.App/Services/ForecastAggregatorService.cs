
using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.Interfaces;

namespace WeatherForecastAggregator.App.Services
{
   public class ForecastAggregatorService : IForecastAggregatorService
   {
      private readonly IEnumerable<IForecastService> _forecastServices;

      public ForecastAggregatorService(IEnumerable<IForecastService> forecastServices)
      {
         _forecastServices = forecastServices;
      }

      public async Task<ForecastsResponse?> GetForecasts(ForecastsRequest request)
      {
         var forecasts = await Task.WhenAll(_forecastServices.Select(s => s.GetForecast(request.Coordinates)));
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
