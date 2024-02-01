using System.Collections.Generic;

namespace WeatherForecastAggregator.Domain.Models
{
   public class AggregatedForecast
   {
      public IEnumerable<ForecastSource> Sources { get; set; }
   }
}
