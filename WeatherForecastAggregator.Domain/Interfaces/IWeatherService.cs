using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Domain.Interfaces
{
   public interface IWeatherService
   {
      Task<ForecastsResponse?> GetForecasts(ForecastsRequest request);
   }
}