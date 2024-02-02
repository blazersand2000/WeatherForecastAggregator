﻿using System.Threading.Tasks;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Domain.Interfaces
{
   public interface IWeatherAggregatorService
   {
      Task<AggregatedForecast> GetForecasts(ForecastsRequest request);
   }
}