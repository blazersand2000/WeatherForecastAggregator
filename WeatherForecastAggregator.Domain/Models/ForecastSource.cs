namespace WeatherForecastAggregator.Domain.Models
{
   public class ForecastSource
   {
      public string Name { get; set; }
      public double CurrentTemperatureF { get; set; }
      public AttributionNode Attribution { get; set; }

      //TODO: Add daily forecasts
   }

   public class AttributionNode
   {
      public string Text { get; set; }
      public string Url { get; set; }
      public string? LogoUrl { get; set; }
   }
}
