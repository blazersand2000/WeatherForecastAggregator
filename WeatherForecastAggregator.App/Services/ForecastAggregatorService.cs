
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

      public async Task<AggregatedForecast> GetAggregatedForecast(Coordinates point)
      {
         var forecasts = await Task.WhenAll(_forecastServices.Select(s => s.GetForecast(point)));
         return new AggregatedForecast
         {
            Sources = forecasts
         };
      }
   }
}
