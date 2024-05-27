using MicroService.Shared.ApiClient;
using MicroService.Shared.Request.Notify;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Telegram.BotAPI;

namespace MicroService.NotifyService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ServiceConfig");

            var telegramBotToken = config.GetSection("TelegramBotToken").Value;
            var botClient = new TelegramBotClient(telegramBotToken);

            var gatewayApiBaseURL = config.GetSection("GatewayApiBaseURL").Value;
            var gatewayApiClient = new GatewayApiClient(gatewayApiBaseURL);

            var botManager = new BotManager(botClient, gatewayApiClient);

            var rabbitMQConfig = config.GetSection("RabbitMQ");
            var rabbitMQHost = rabbitMQConfig.GetSection("Host").Value;

            var factory = new ConnectionFactory() { HostName = rabbitMQHost };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "NotifyQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // on message received
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    var notifyRequest = JsonConvert.DeserializeObject<NotifyUserRequest>(message);
                    botManager.Notify(notifyRequest);
                    channel.BasicAck(ea.DeliveryTag, false);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            };
            channel.BasicConsume(queue: "NotifyQueue", autoAck: false, consumer: consumer);
            while (true)
            {
                Thread.Sleep(100);
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
