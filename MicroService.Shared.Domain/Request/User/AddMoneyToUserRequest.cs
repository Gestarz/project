namespace MicroService.Shared.Request.User
{
    public class AddMoneyToUserRequest
    {
        public Guid Guid { get; set; }
        public int Amount { get; set; }

        public string SecretToken { get; set; }
    }
}
