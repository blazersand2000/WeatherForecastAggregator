using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

public interface IForecastService
{
   Task<string> GetForecast(Coordinates point);
}