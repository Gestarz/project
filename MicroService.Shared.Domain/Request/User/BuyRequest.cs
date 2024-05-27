namespace MicroService.Shared.Request.User
{
    public class BuyRequest
    {
        public Guid UserGuid { get; set; }
        public int Amount { get; set; }
        public Guid ItemGuid { get; set; }
    }
}
