namespace MicroService.Domain.Class
{
    public class User : Entity
    {

        public string Name { get; set; } = "default name";
        public long Balance { get; set; }
        public long? TelegramChatId { get; set; }

    }
}
