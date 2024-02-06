using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.DTOs.BingMaps;

namespace WeatherForecastAggregator.Infrastructure.Converters
{
    public class BingTimeZoneOffsetConverter : IConverter<TimeZoneResponseDto, TimeZoneInfo>
    {
      public TimeZoneInfo Convert(TimeZoneResponseDto source)
      {
         throw new NotImplementedException();
      }
   }
}
