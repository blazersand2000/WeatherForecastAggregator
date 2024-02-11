using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.Interfaces;

namespace WeatherForecastAggregator.App.Services.Forecasts;

public class NationalWeatherService : IForecastService
{
   private readonly INationalWeatherServiceDataProvider _dataProvider;

   public NationalWeatherService(INationalWeatherServiceDataProvider dataProvider)
   {
      _dataProvider = dataProvider;
   }

   public async Task<ForecastSource> GetForecast(Coordinates point, TimeZoneInfo timeZoneInfo)
   {
      var dto = await _dataProvider.GetForecast(point, timeZoneInfo);

      var periodsGroupedByDay = dto.Properties.Periods.GroupBy(p => p.StartTime.Date);
      var dailyForecasts = periodsGroupedByDay.Select(g => new DailyForecast
      {
         Date = g.Key.ToString("yyyy-MM-dd"),
         HighTemperatureF = g.Max(p => p.Temperature),
         LowTemperatureF = g.Min(p => p.Temperature),
         ShortForecast = string.Join(", ", g.Select(p => p.ShortForecast).Distinct()),
         ProbabilityOfPrecipitation = g.Max(p => p.ProbabilityOfPrecipitation.Value / 100f),
         WindSpeed = g.Max(p => int.Parse(p.WindSpeed.Split(' ')[0]))
      });

      var forecast = new ForecastSource
      {
         Name = "National Weather Service",
         Attribution = new AttributionNode
         {
            Text = "National Weather Service",
            Url = "https://www.weather.gov/documentation/services-web-api",
            LogoUrl = ""
         },
         DailyForecasts = dailyForecasts.ToList()
      };

      return forecast;

   }
}