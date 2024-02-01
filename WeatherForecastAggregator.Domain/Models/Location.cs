namespace WeatherForecastAggregator.Domain.Models
{
   public class Location
   {
      public string Name { get; set; }
      public Coordinates Coordinates { get; set; }
   }

   public class Coordinates
   {
      public double Latitude { get; set; }
      public double Longitude { get; set; }
   }
}
