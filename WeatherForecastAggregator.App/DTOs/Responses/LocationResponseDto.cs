using System;

namespace WeatherForecastAggregator.Domain.Models
{
   public record LocationResponseDto(string Name, Coordinates Coordinates, TimeZoneInfo TimeZone);
}
