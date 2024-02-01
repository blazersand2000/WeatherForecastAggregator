using AutoMapper;
using WeatherForecastAggregator.Domain.Models;
using WeatherForecastAggregator.Infrastructure.DTOs;

namespace WeatherForecastAggregator.App.Mappings
{
   public class InfraMapperProfile : Profile
   {
      public InfraMapperProfile()
      {
         CreateMap<BingMapsResponseDto, Location>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ResourceSets[0].Resources[0].Name))
            .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => new Coordinates
            {
               Latitude = src.ResourceSets[0].Resources[0].Point.Coordinates[0],
               Longitude = src.ResourceSets[0].Resources[0].Point.Coordinates[1]
            }));
      }
   }
}
