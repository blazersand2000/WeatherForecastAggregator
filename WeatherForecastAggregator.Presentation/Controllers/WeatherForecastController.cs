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
      private readonly ILocationService _locationService;
      private readonly IForecastAggregatorService _forecastService;
      private readonly IMapper _mapper;
      private readonly ILogger<WeatherForecastController> _logger;

      public WeatherForecastController(ILocationService locationService, IForecastAggregatorService forecastService, IMapper mapper, ILogger<WeatherForecastController> logger)
      {
         _locationService = locationService;
         _forecastService = forecastService;
         _mapper = mapper;
         _logger = logger;
      }

      [HttpGet("location")]
      public async Task<ActionResult<LocationResponseDto>> GetLocation(string? search)
      {
         if (string.IsNullOrWhiteSpace(search))
         {
            return BadRequest("Location is required.");
         }

         var location = await _locationService.GetLocation(search);
         if (location == null)
         {
            return NotFound($"\"{search}\" not found.");
         }

         var response = _mapper.Map<LocationResponseDto>(location);

         return response;
      }

      [HttpGet("forecasts")]
      public async Task<ActionResult<ForecastsResponseDto>> GetForecasts(double? lat, double? lon)
      {
         if (lat == null || lon == null)
         {
            return BadRequest("Valid coordinates were not received but are required to get forecasts.");
         }
         
         var requestDto = new ForecastsRequestDto { Coordinates = new Coordinates(lat.Value, lon.Value) };
         var request = _mapper.Map<ForecastsRequest>(requestDto);

         var forecasts = await _forecastService.GetForecasts(request);
         if (forecasts == null)
         {
            return NotFound("Forecasts not found.");
         }

         var response = _mapper.Map<ForecastsResponseDto>(forecasts);

         return response;
      }
   }
}