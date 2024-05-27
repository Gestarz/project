namespace MicroService.Shared.Request.Item
{
    public class BuyItemRequest
    {
        public Guid Guid { get; set; }
        public int Amount { get; set; }
    }
}
