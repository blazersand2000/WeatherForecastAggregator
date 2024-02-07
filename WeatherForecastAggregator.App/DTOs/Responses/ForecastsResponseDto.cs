namespace WeatherForecastAggregator.Domain.Models
{
   public class ForecastsResponseDto
   {
      public IEnumerable<ForecastSource> Sources { get; set; }
      //public string LocationName { get; set; }
   }
}
