using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.DTOs.OpenWeatherMap;

namespace WeatherForecastAggregator.Infrastructure.Interfaces
{
   public interface IOpenWeatherMapDataProvider
   {
      Task<ForecastResponseDto> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo);
   }
}