using MicroService.Domain.Class;
using MicroService.Domain.Repository;
using MicroService.ItemApi.Manager;
using Microsoft.EntityFrameworkCore;

namespace MicroService.ItemApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        // configure services
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSingleton<ItemManager>();


            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ServiceConfig");
            var dbConnectionString = config.GetSection("Database").Value;

            // create DbContext from connection string
            var context = new DatabaseContext(
                new DbContextOptionsBuilder<DatabaseContext>().UseNpgsql(dbConnectionString).Options
            );

            services.AddSingleton<DatabaseContext>(context);

            services.AddSingleton<IRepository<Item>, DBRepository<Item>>();
        }
    }
}
