namespace MicroService.Shared.Request.Item
{
    public class UpdateAmountRequest
    {
        public Guid Guid { get; set; }
        public int Amount { get; set; }
    }
}
