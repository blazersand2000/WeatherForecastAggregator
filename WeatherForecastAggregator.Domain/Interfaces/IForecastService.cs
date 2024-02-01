using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Domain.Interfaces
{
   public interface IForecastService
   {
      Task<AggregatedForecast> GetForecasts(ForecastsRequest request);
   }
}