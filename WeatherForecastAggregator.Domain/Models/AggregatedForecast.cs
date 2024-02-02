using System.Collections.Generic;

namespace WeatherForecastAggregator.Domain.Models
{
   public class AggregatedForecast
   {
      public string Location { get; set; }
      public IEnumerable<ForecastSource> Sources { get; set; }
   }
}
