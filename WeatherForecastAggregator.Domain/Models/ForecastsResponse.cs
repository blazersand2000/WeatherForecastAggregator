using System.Collections.Generic;

namespace WeatherForecastAggregator.Domain.Models
{
   public class ForecastsResponse
   {
      public IEnumerable<ForecastSource> Sources { get; set; }
      //public string LocationName { get; set; }
   }
}
