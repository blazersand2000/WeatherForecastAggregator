namespace WeatherForecastAggregator.Domain.Models
{
   public class ForecastsResponse
   {
      public AggregatedForecast AggregatedForecast { get; set; }
      public string LocationName { get; set; }
   }
}
