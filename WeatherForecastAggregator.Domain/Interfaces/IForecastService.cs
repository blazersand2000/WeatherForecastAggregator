using System;
using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Domain.Interfaces
{
   public interface IForecastService
   {
      Task<ForecastSource> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo);
   }
}