using System.Text.Json.Serialization;

namespace WeatherForecastAggregator.Infrastructure.DTOs.NWS
{
   public class PointResponseDto
   {
      [JsonPropertyName("@context")]
      public List<object> Context { get; set; }
      public string Id { get; set; }
      public string Type { get; set; }
      public GeometryDto Geometry { get; set; }
      public PropertiesDto Properties { get; set; }

      public class BearingDto
      {
         public string UnitCode { get; set; }
         public int Value { get; set; }
      }

      public class DistanceDto
      {
         public string UnitCode { get; set; }
         public double Value { get; set; }
      }

      public class GeometryDto
      {
         public string Type { get; set; }
         public List<double> Coordinates { get; set; }
      }

      public class PropertiesDto
      {
         [JsonPropertyName("@id")]
         public string Id { get; set; }

         [JsonPropertyName("@type")]
         public string Type { get; set; }
         public string Cwa { get; set; }
         public string ForecastOffice { get; set; }
         public string GridId { get; set; }
         public int GridX { get; set; }
         public int GridY { get; set; }
         public string Forecast { get; set; }
         public string ForecastHourly { get; set; }
         public string ForecastGridData { get; set; }
         public string ObservationStations { get; set; }
         public RelativeLocationDto RelativeLocation { get; set; }
         public string ForecastZone { get; set; }
         public string County { get; set; }
         public string FireWeatherZone { get; set; }
         public string TimeZone { get; set; }
         public string RadarStation { get; set; }
         public string City { get; set; }
         public string State { get; set; }
         public DistanceDto Distance { get; set; }
         public BearingDto Bearing { get; set; }
      }

      public class RelativeLocationDto
      {
         public string Type { get; set; }
         public GeometryDto Geometry { get; set; }
         public PropertiesDto Properties { get; set; }
      }

   }
}
