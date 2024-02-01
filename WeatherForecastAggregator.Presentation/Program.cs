using WeatherForecastAggregator.App.Mappings;
using WeatherForecastAggregator.App.Services;
using WeatherForecastAggregator.Domain.Interfaces;

namespace WeatherForecastAggregator
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         // Add services to the container.
         ConfigureServices(builder.Services);

         builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

         builder.Services.AddControllers();
         // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();

         var app = builder.Build();

         // Configure the HTTP request pipeline.
         if (app.Environment.IsDevelopment())
         {
            app.UseSwagger();
            app.UseSwaggerUI();
         }

         app.UseHttpsRedirection();

         app.UseAuthorization();


         app.MapControllers();

         app.Run();
      }

      static void ConfigureServices(IServiceCollection services)
      {
         services.AddSingleton<IForecastService, ForecastService>();
      }
   }
}