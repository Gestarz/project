using MicroService.Shared.ApiClient;
using MicroService.Shared.Request.Notify;
using MicroService.Shared.Request.User;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace MicroService.NotifyService
{
    public class BotManager
    {
        private readonly TelegramBotClient BotClient;
        private readonly GatewayApiClient GatewayApiClient;

        public BotManager(TelegramBotClient botClient, GatewayApiClient gatewayApiClient)
        {
            BotClient = botClient;
            GatewayApiClient = gatewayApiClient;
        }

        public object Notify(NotifyUserRequest request)
        {
            try
            {
                var user = GatewayApiClient.GetUser(new GetUserRequest { Guid = request.Guid });
                if (user == null || user.TelegramChatId == null)
                {
                    return new { Message = "User not found or TelegramChatId is empty!" };
                }
                return BotClient.SendMessage(new SendMessageArgs((long)user.TelegramChatId, request.Message));
            }
            catch (Exception ex)
            {
                return new { Message = $"Error: {ex.InnerException}" };
            }
        }
    }
}
