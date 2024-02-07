using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.App.DTOs.Requests
{
   public class ForecastsRequestDto
   {
      public Coordinates Coordinates { get; set; }
   }
}
