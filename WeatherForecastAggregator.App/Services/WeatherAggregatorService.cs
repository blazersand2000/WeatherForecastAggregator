using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.App.Services
{
   public class WeatherAggregatorService : IWeatherAggregatorService
   {
      private readonly IGeocodeService _geocodeService;
      private readonly IEnumerable<IForecastService> _forecastServices;

      public WeatherAggregatorService(IGeocodeService geocodeService, IEnumerable<IForecastService> forecastServices)
      {
         _geocodeService = geocodeService;
         _forecastServices = forecastServices;
      }

      public async Task<AggregatedForecast> GetForecasts(ForecastsRequest request)
      {
         var location = await _geocodeService.Geocode(request.Location);
         var forecastTasks = _forecastServices.Select(fs => fs.GetForecast(location.Coordinates));
         var forecasts = await Task.WhenAll(forecastTasks);
         var sources = forecasts.Select(f => new ForecastSource
         {
            Name = f,
            CurrentTemperatureF = 69
         });
         return new AggregatedForecast
         {
            Location = location.Name,
            Sources = sources
         };
      }
   }
}