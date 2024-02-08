using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.App.Services
{
   public class LocationService : ILocationService
   {
      private readonly IGeocodeService _geocodeService;

      public LocationService(IGeocodeService geocodeService)
      {
         _geocodeService = geocodeService;
      }

      public async Task<Location?> GetLocation(string location)
      {
         var geoLocationDto = await _geocodeService.Geocode(location);
         if (geoLocationDto?.ResourceSets == null || !geoLocationDto.ResourceSets.Any() ||
             geoLocationDto.ResourceSets[0]?.Resources == null || !geoLocationDto.ResourceSets[0].Resources.Any())
         {
            return null;
         }

         var geoLocation = geoLocationDto.ResourceSets[0].Resources[0];

         var (lat, lon) = (geoLocation.Point.Coordinates[0], geoLocation.Point.Coordinates[1]);
         var timeZoneInfo = await GetTimeZoneInfo(lat, lon);
         if (timeZoneInfo == null)
         {
            return null;
         }

         return new Location(geoLocation.Name, new Coordinates(lat, lon), timeZoneInfo);
      }

      public async Task<TimeZoneInfo?> GetTimeZoneInfo(double lat, double lon)
      {
         var timeZoneDto = await _geocodeService.GetTimeZoneInfo(lat, lon);
         if (timeZoneDto?.ResourceSets == null || !timeZoneDto.ResourceSets.Any() ||
             timeZoneDto.ResourceSets[0]?.Resources == null || !timeZoneDto.ResourceSets[0].Resources.Any())
         {
            return null;
         }

         var timeZone = timeZoneDto.ResourceSets[0].Resources[0];
         var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone.TimeZone.WindowsTimeZoneId ?? "UTC") ?? TimeZoneInfo.Utc;
         return timeZoneInfo;
      }
   }
}
