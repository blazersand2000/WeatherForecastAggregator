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
         var geoLocation = (await _geocodeService.Geocode(location))?.ResourceSets[0]?.Resources[0];
         if (geoLocation == null)
         {
            return null;
         }

         var (lat, lon) = (geoLocation.Point.Coordinates[0], geoLocation.Point.Coordinates[1]);
         var timeZone = (await _geocodeService.GetTimeZoneInfo(lat, lon))?.ResourceSets[0]?.Resources[0];
         if (timeZone == null)
         {
            return null;
         }

         var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone?.TimeZone.WindowsTimeZoneId ?? "UTC");

         return new Location(geoLocation.Name, new Coordinates(lat, lon), timeZoneInfo);
      }
   }
}
