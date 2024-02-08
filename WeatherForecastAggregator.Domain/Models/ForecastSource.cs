using System;
using System.Collections.Generic;

namespace WeatherForecastAggregator.Domain.Models
{
   public class ForecastSource
   {
      public string Name { get; set; }
      public AttributionNode Attribution { get; set; }
      public List<DailyForecast> DailyForecasts { get; set; }
   }

   public class AttributionNode
   {
      public string Text { get; set; }
      public string Url { get; set; }
      public string? LogoUrl { get; set; }
   }

   public class DailyForecast
   {
      public string Date { get; set; }
      public double HighTemperatureF { get; set; }
      public double LowTemperatureF { get; set; }
      public string ShortForecast { get; set; }
      //public string DetailedForecast { get; set; }
      public double ProbabilityOfPrecipitation { get; set; }
      public int WindSpeed { get; set; }
   }
}
