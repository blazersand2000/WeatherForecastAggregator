using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Domain.Interfaces;

public interface IForecastAggregatorService
{
   Task<ForecastsResponse?> GetForecasts(ForecastsRequest request);
}