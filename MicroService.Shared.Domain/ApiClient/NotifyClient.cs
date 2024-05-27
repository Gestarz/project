using MicroService.Shared.Request.Notify;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MicroService.Shared.ApiClient
{
    public class NotifyClient
    {
        private ConnectionFactory Factory { get; set; }

        public NotifyClient(string baseURL)
        {
            Factory = new ConnectionFactory() { HostName = baseURL };
        }

        public void NotifyUser(NotifyUserRequest request)
        {

            using (var connection = Factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "NotifyQueue",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var message = JsonConvert.SerializeObject(request);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: "NotifyQueue",
                               basicProperties: null,
                               body: body);
            }
        }
    }
}
