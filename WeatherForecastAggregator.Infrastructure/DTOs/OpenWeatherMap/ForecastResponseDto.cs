using System.Text.Json.Serialization;

namespace WeatherForecastAggregator.Infrastructure.DTOs.OpenWeatherMap
{
   public class ForecastResponseDto
   {
      public string Cod { get; set; }
      public int Message { get; set; }
      public int Cnt { get; set; }
      public List<ListDto> List { get; set; }
      public CityDto City { get; set; }

      public class CityDto
      {
         public int Id { get; set; }
         public string Name { get; set; }
         public CoordDto Coord { get; set; }
         public string Country { get; set; }
         public int Population { get; set; }
         public int Timezone { get; set; }
         public int Sunrise { get; set; }
         public int Sunset { get; set; }
      }

      public class CloudsDto
      {
         public int All { get; set; }
      }

      public class CoordDto
      {
         public double Lat { get; set; }
         public double Lon { get; set; }
      }

      public class ListDto
      {
         public int Dt { get; set; }
         public MainDto Main { get; set; }
         public List<WeatherDto> Weather { get; set; }
         public CloudsDto Clouds { get; set; }
         public WindDto Wind { get; set; }
         public int Visibility { get; set; }
         public double Pop { get; set; }
         public SysDto Sys { get; set; }
         public string DtTxt { get; set; }
         public RainDto Rain { get; set; }
      }

      public class MainDto
      {
         public double Temp { get; set; }
         public double FeelsLike { get; set; }
         public double TempMin { get; set; }
         public double TempMax { get; set; }
         public int Pressure { get; set; }
         public int SeaLevel { get; set; }
         public int GrndLevel { get; set; }
         public int Humidity { get; set; }
         public double TempKf { get; set; }
      }

      public class RainDto
      {
         [JsonPropertyName("3h")]
         public double _3h { get; set; }
      }

      public class SysDto
      {
         public string Pod { get; set; }
      }

      public class WeatherDto
      {
         public int Id { get; set; }
         public string Main { get; set; }
         public string Description { get; set; }
         public string Icon { get; set; }
      }

      public class WindDto
      {
         public double Speed { get; set; }
         public int Deg { get; set; }
         public double Gust { get; set; }
      }
   }
}