using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.Interfaces;

namespace WeatherForecastAggregator.App.Services.Forecasts;

public class OpenWeatherMapService : IForecastService
{
   private readonly IOpenWeatherMapDataProvider _dataProvider;

   public OpenWeatherMapService(IOpenWeatherMapDataProvider dataProvider)
   {
      _dataProvider = dataProvider;
   }

   public async Task<ForecastSource> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo)
   {
      var dto = await _dataProvider.GetForecast(point, timeZoneInfo);

      var periodsGroupedByDay = dto.List
         .GroupBy(p => TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.FromUnixTimeSeconds(p.Dt).UtcDateTime, timeZoneInfo).Date);

      var dailyForecasts = periodsGroupedByDay.Select(g => new DailyForecast
      {
         Date = g.Key.ToString("yyyy-MM-dd"),
         HighTemperatureF = g.Max(p => p.Main.Temp),
         LowTemperatureF = g.Min(p => p.Main.Temp),
         ShortForecast = string.Join(", ", g.SelectMany(p => p.Weather).Select(w => w.Description).Distinct()),
         ProbabilityOfPrecipitation = g.Max(p => p.Pop),
         WindSpeed = g.Max(p => (int)Math.Round(p.Wind.Speed))
      });

      var forecast = new ForecastSource
      {
         Name = "OpenWeatherMap",
         Attribution = new AttributionNode
         {
            Text = "Weather data provided by OpenWeather",
            Url = "https://openweathermap.org/",
            LogoUrl = "/OpenWeather-Master-Logo RGB.png"
         },
         DailyForecasts = dailyForecasts.ToList()
      };

      return forecast;
   }
}