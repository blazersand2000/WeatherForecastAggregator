using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.App.Services
{
   public class ForecastService : IForecastService
   {
      public async Task<AggregatedForecast> GetForecasts(ForecastsRequest request)
      {
         return new AggregatedForecast
         {
            Sources = new[]
            {
               new ForecastSource
               {
                  Name = "WeatherSource1",
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