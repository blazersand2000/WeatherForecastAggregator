namespace WeatherForecastAggregator.Infrastructure.DTOs.BingMaps
{
   public class TimeZoneResponseDto
   {
      public string AuthenticationResultCode { get; set; }
      public string BrandLogoUri { get; set; }
      public string Copyright { get; set; }
      public List<ResourceSet> ResourceSets { get; set; }
      public int StatusCode { get; set; }
      public string StatusDescription { get; set; }
      public string TraceId { get; set; }

      public class ConvertedTime
      {
         public DateTime LocalTime { get; set; }
         public string UtcOffsetWithDst { get; set; }
         public string TimeZoneDisplayName { get; set; }
         public string TimeZoneDisplayAbbr { get; set; }
      }

      public class Resource
      {
         public string Type { get; set; }
         public TimeZone TimeZone { get; set; }
      }

      public class ResourceSet
      {
         public int EstimatedTotal { get; set; }
         public List<Resource> Resources { get; set; }
      }

      public class TimeZone
      {
         public string GenericName { get; set; }
         public string Abbreviation { get; set; }
         public string IanaTimeZoneId { get; set; }
         public string WindowsTimeZoneId { get; set; }
         public string UtcOffset { get; set; }
         public ConvertedTime ConvertedTime { get; set; }
      }
   }
}
