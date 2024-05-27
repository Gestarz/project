namespace MicroService.Domain.Class
{
    public class Transaction : Entity
    {

        public Guid UserGuid { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
