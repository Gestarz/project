namespace MicroService.Shared.Request.User
{
    public class UpdateUserRequest
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public long TelegramChatId { get; set; }
    }
}
