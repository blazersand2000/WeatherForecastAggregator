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
      private readonly IWeatherService _forecastService;
      private readonly IMapper _mapper;
      private readonly ILogger<WeatherForecastController> _logger;

      public WeatherForecastController(IWeatherService forecastService, IMapper mapper, ILogger<WeatherForecastController> logger)
      {
         _forecastService = forecastService;
         _mapper = mapper;
         _logger = logger;
      }

      [HttpGet]
      public async Task<ForecastsResponseDto> Get(string location)
      {
         var requestDto = new ForecastsRequestDto { Location = location };
         var request = _mapper.Map<ForecastsRequest>(requestDto);

         var forecasts = await _forecastService.GetForecasts(request);

         var responseDto = _mapper.Map<ForecastsResponseDto>(forecasts);

         return responseDto;
      }
   }
}