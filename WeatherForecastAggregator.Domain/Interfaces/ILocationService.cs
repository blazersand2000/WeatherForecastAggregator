using System;
using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Domain.Interfaces;

public interface ILocationService
{
   Task<Location?> GetLocation(string location);
   Task<TimeZoneInfo?> GetTimeZoneInfo(double lat, double lon);
}