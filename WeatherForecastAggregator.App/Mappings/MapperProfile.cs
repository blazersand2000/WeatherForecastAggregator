using AutoMapper;
using WeatherForecastAggregator.App.DTOs.Requests;
using WeatherForecastAggregator.Domain.Models;

namespace WeatherForecastAggregator.App.Mappings
{
   public class MapperProfile : Profile
   {
      public MapperProfile()
      {
         CreateMap<ForecastsRequest, ForecastsRequestDto>().ReverseMap();
         CreateMap<ForecastsResponse, ForecastsResponseDto>().ReverseMap();
      }
   }
}
