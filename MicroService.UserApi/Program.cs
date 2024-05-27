using MicroService.Domain.Cache;
using MicroService.Domain.Class;
using MicroService.Domain.Repository;
using MicroService.Shared.ApiClient;
using MicroService.UserApi.Manager;
using Microsoft.EntityFrameworkCore;

namespace MicroService.UserApi
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

            services.AddSingleton<ResponseManager>();
            services.AddSingleton<NodeManager>();

            services.AddSingleton<UserManager>();
            services.AddSingleton<TokenManager>();


            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ServiceConfig");
            var dbConnectionString = config.GetSection("Database").Value;
            var redisConnectionString = config.GetSection("Redis").Value;
            var gatewayUrl = config.GetSection("GatewayApi").Value;

            // create DbContext from connection string
            var context = new DatabaseContext(
                new DbContextOptionsBuilder<DatabaseContext>().UseNpgsql(dbConnectionString).Options
            );
            services.AddSingleton<DatabaseContext>(context);
            // add IDistributedCache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "userapi";
            });

            var gatewayApiClient = new GatewayApiClient(gatewayUrl);


            services.AddSingleton<ICacheRepository, RedisCacheRepository>();
            services.AddSingleton<IRepository<User>, DBRepository<User>>();
            services.AddSingleton(gatewayApiClient);

        }
    }
}
