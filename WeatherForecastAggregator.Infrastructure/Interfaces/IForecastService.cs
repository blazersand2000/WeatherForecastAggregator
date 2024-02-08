using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Infrastructure.Interfaces
{
   public interface IForecastService
   {
      Task<ForecastSource> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo);
   }
}