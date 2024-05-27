using MicroService.Gateway.Manager;
using MicroService.Shared.ApiClient;

namespace MicroService.Gateway
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

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ServiceConfig");
            var itemApi = config.GetSection("ItemApi").Value;
            var userApiList = config.GetSection("UserApi");
            var userApiClientList = new List<UserApiClient>();
            foreach (var userApi in userApiList.GetChildren())
            {
                userApiClientList.Add(new UserApiClient(userApi.Value));
            }

            var rabbitMq = config.GetSection("RabbitMQ");
            var RMQHost = rabbitMq.GetSection("Host").Value;

            var userApiClientManager = new UserApiClientLoadManager(userApiClientList);
            var itemApiClient = new ItemApiClient(itemApi);
            var notifyApiClient = new NotifyClient(RMQHost);

            services.AddSingleton<UserApiClientLoadManager>(userApiClientManager);
            services.AddSingleton<ItemApiClient>(itemApiClient);
            services.AddSingleton<NotifyClient>(notifyApiClient);
            services.AddSingleton<ProxyCallManager>();


        }
    }
}
