using AutoMapper;
using WeatherForecastAggregator.Infrastructure.Converters;

namespace WeatherForecastAggregator.Tests.Mappings.Converters
{
    public class Tests
   {
      private BingTimeZoneOffsetConverter _bingTimeZoneOffsetConverter;
      private ResolutionContext _resolutionContext;
      
      [SetUp]
      public void Setup()
      {
         _bingTimeZoneOffsetConverter = new BingTimeZoneOffsetConverter();
         //_resolutionContext = 
         // create mapperconfiguration and get its resolutioncontext


      }

      //[Test]
      //[TestCase("-7:30", "-7:30")]
      //public void Convert(string source, TimeSpan expected)
      //{
      //   var result = _bingTimeZoneOffsetConverter.Convert(source, null);
      //}
   }
}