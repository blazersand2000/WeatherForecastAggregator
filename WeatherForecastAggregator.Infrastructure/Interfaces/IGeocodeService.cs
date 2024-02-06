
using WeatherForecastAggregator.Infrastructure.DTOs.BingMaps;

public interface IGeocodeService
{
   Task<BingMapsResponseDto?> Geocode(string location);
   Task<TimeZoneResponseDto?> GetTimeZoneInfo(double latitude, double longitude);
}
