namespace WeatherForecastAggregator.Domain.Models
{
   public class AggregatedForecastDto
   {
      public IEnumerable<ForecastSourceDto> Sources { get; set; }
   }
}
