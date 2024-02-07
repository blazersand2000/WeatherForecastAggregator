﻿using System.Text.Json.Serialization;

namespace WeatherForecastAggregator.Infrastructure.DTOs.NWS
{
   public class ForecastResponseHourlyDto
   {
      [JsonPropertyName("@context")]
      public List<object> Context { get; set; }
      public string Type { get; set; }
      public GeometryDto Geometry { get; set; }
      public PropertiesDto Properties { get; set; }

      public class DewpointDto
      {
         public string UnitCode { get; set; }
         public double Value { get; set; }
      }

      public class ElevationDto
      {
         public string UnitCode { get; set; }
         public double Value { get; set; }
      }

      public class GeometryDto
      {
         public string Type { get; set; }
         public List<List<List<double>>> Coordinates { get; set; }
      }

      public class PeriodDto
      {
         public int Number { get; set; }
         public string Name { get; set; }
         public DateTime StartTime { get; set; }
         public DateTime EndTime { get; set; }
         public bool IsDaytime { get; set; }
         public int Temperature { get; set; }
         public string TemperatureUnit { get; set; }
         public object TemperatureTrend { get; set; }
         public ProbabilityOfPrecipitationDto ProbabilityOfPrecipitation { get; set; }
         public DewpointDto Dewpoint { get; set; }
         public RelativeHumidityDto RelativeHumidity { get; set; }
         public string WindSpeed { get; set; }
         public string WindDirection { get; set; }
         public string Icon { get; set; }
         public string ShortForecast { get; set; }
         public string DetailedForecast { get; set; }
      }

      public class ProbabilityOfPrecipitationDto
      {
         public string UnitCode { get; set; }
         public int Value { get; set; }
      }

      public class PropertiesDto
      {
         public DateTime Updated { get; set; }
         public string Units { get; set; }
         public string ForecastGenerator { get; set; }
         public DateTime GeneratedAt { get; set; }
         public DateTime UpdateTime { get; set; }
         public string ValidTimes { get; set; }
         public ElevationDto Elevation { get; set; }
         public List<PeriodDto> Periods { get; set; }
      }

      public class RelativeHumidityDto
      {
         public string UnitCode { get; set; }
         public int Value { get; set; }
      }

   }
}
