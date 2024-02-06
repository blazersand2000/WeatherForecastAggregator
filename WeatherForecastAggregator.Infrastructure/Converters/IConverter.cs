namespace WeatherForecastAggregator.Infrastructure.Converters
{
   internal interface IConverter<TSource, TDest>
   {
      TDest Convert(TSource source);
   }
}
