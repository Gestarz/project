namespace MicroService.Shared.Request.User
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public long TelegramChatId { get; set; }
    }
}
