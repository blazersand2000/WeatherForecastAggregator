using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.DTOs.NWS;

namespace WeatherForecastAggregator.Infrastructure.Interfaces
{
   public interface INationalWeatherServiceDataProvider
   {
      Task<ForecastResponseHourlyDto> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo);
   }
}