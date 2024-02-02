namespace WeatherForecastAggregator.Domain.Models
{
   public class AggregatedForecastDto
   {
      public string Location { get; set; }
      public IEnumerable<ForecastSourceDto> Sources { get; set; }
   }
}
