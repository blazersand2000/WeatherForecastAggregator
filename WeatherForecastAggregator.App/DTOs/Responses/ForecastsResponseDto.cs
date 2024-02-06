namespace WeatherForecastAggregator.Domain.Models
{
   public class ForecastsResponseDto
   {
      public AggregatedForecast AggregatedForecast { get; set; }
      public string LocationName { get; set; }
   }
}
