using System;

namespace WeatherForecastAggregator.Domain.Models
{
   public record Location(string Name, Coordinates Coordinates, TimeZoneInfo TimeZone);
}
