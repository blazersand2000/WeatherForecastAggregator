using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastAggregator.App.DTOs.Requests;
using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class WeatherForecastController : ControllerBase
   {
      private readonly IWeatherAggregatorService _forecastService;
      private readonly IMapper _mapper;
      private readonly ILogger<WeatherForecastController> _logger;

      public WeatherForecastController(IWeatherAggregatorService forecastService, IMapper mapper, ILogger<WeatherForecastController> logger)
      {
         _forecastService = forecastService;
         _mapper = mapper;
         _logger = logger;
      }

      [HttpGet]
      public async Task<AggregatedForecastDto> Get(string location)
      {
         var requestDto = new ForecastsRequestDto { Location = location };
         var request = _mapper.Map<ForecastsRequest>(requestDto);

         var forecasts = await _forecastService.GetForecasts(request);

         var responseDto = _mapper.Map<AggregatedForecastDto>(forecasts);

         return responseDto;
      }
   }
}