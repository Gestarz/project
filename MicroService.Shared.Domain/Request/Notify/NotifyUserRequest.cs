namespace MicroService.Shared.Request.Notify
{
    public class NotifyUserRequest
    {
        public Guid Guid { get; set; }
        public string Message { get; set; }
    }
}
