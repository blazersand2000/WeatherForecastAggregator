using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Domain.Interfaces;

public interface ILocationService
{
   Task<Location?> GetLocation(string location);
}