using Microsoft.Extensions.DependencyInjection;
using WeatherForecastAggregator.App.Mappings;
using WeatherForecastAggregator.App.Services;
using WeatherForecastAggregator.App.Services.Forecasts;
using WeatherForecastAggregator.Domain.Interfaces;
using WeatherForecastAggregator.Infrastructure.Interfaces;
using WeatherForecastAggregator.Infrastructure.Options;
using WeatherForecastAggregator.Infrastructure.Services.APIs.Geocoding;

namespace WeatherForecastAggregator
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         builder.Services.Configure<BingMapsOptions>(builder.Configuration.GetSection("BingMaps"));
         builder.Services.Configure<OpenWeatherMapOptions>(builder.Configuration.GetSection("OpenWeatherMap"));

         ConfigureServices(builder.Services);

         builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly, typeof(InfraMapperProfile).Assembly);
         builder.Services.AddMemoryCache();

         AddNamedHttpClients(builder);

         builder.Services.AddControllers();
         // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();

         builder.Services.AddCors(options =>
         {
            options.AddPolicy("AllowVueApp",
                builder =>
                {
                   builder.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
                });
         });

         var app = builder.Build();

         // Configure the HTTP request pipeline.
         if (app.Environment.IsDevelopment())
         {
            app.UseSwagger();
            app.UseSwaggerUI();
         }

         app.UseHttpsRedirection();

         app.UseCors("AllowVueApp");

         app.UseStaticFiles();

         app.UseSpa(spa =>
         {
            spa.Options.SourcePath = "wwwroot";
         });

         app.UseAuthorization();

         app.MapControllers();

         app.Run();
      }

      private static void AddNamedHttpClients(WebApplicationBuilder builder)
      {
         builder.Services.AddHttpClient<IGeocodeService, BingMapsLocationsApi>(client =>
         {
            client.BaseAddress = new Uri(builder.Configuration["BingMaps:BaseAddress"]);
         });

         builder.Services.AddHttpClient<INationalWeatherServiceDataProvider, NationalWeatherServiceApi>(client =>
         {
            client.BaseAddress = new Uri("https://api.weather.gov");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherForecastAggregator");
         });

         builder.Services.AddHttpClient<IOpenWeatherMapDataProvider, OpenWeatherMapApi>(client =>
         {
            client.BaseAddress = new Uri("https://api.openweathermap.org");
         });
      }

      private static void ConfigureServices(IServiceCollection services)
      {
         services.AddSingleton<IForecastAggregatorService, ForecastAggregatorService>();
         services.AddSingleton<ILocationService, LocationService>();
         services.AddSingleton<IForecastService, NationalWeatherService>();
         services.AddSingleton<IForecastService, OpenWeatherMapService>();
      }
   }
}