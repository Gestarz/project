namespace MicroService.Shared.Request.Item
{
    public class CreateItemRequest
    {
        public string Name { get; set; }
        public int Price { get; set; } = 0;
    }
}
