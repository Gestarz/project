using MicroService.NotifyApi.Manager;
using MicroService.Shared.ApiClient;
using Telegram.BotAPI;

namespace MicroService.NotifyApi
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

            var telegramBotToken = config.GetSection("TelegramBotToken").Value;
            var botClient = new TelegramBotClient(telegramBotToken);
            services.AddSingleton(botClient);

            var gatewayApiBaseURL = config.GetSection("GatewayApiBaseURL").Value;
            var gatewayApiClient = new GatewayApiClient(gatewayApiBaseURL);

            services.AddSingleton(botClient);
            services.AddSingleton(gatewayApiClient);

            services.AddSingleton<BotManager>();

        }
    }
}
