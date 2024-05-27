using MicroService.Domain.Class;
using MicroService.Domain.Repository;
using MicroService.Shared.Request.Item;

namespace MicroService.ItemApi.Manager
{
    public class ItemManager
    {
        IRepository<Item> ItemRepository;
        DatabaseContext DatabaseContext;
        public ItemManager(IRepository<Item> itemRepository, DatabaseContext databaseContext)
        {
            ItemRepository = itemRepository;
            DatabaseContext = databaseContext;
        }

        // get by guid

        public Item GetItem(GetItemRequest request)
        {
            return ItemRepository.GetByGuid(request.Guid);
        }

        public IEnumerable<Item> GetAll()
        {
            return ItemRepository.GetAll();
        }

        // create item using request
        public Item CreateItem(CreateItemRequest request)
        {
            var item = new Item
            {
                Guid = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Amount = 100
            };

            ItemRepository.Add(item);
            return item;
        }

        public bool BuyItem(BuyItemRequest request)
        {
            var item = ItemRepository.GetByGuid(request.Guid);
            if (request.Amount < 1)
            {
                throw new Exception("Amount must be greater than 0");
            }
            if (item == null)
            {
                return false;
            }

            item.Amount -= request.Amount;
            ItemRepository.Update(item);
            return true;
        }

        public Item UpdateAmount(UpdateAmountRequest request)
        {
            var item = ItemRepository.GetByGuid(request.Guid);
            if (request.Amount < 1)
            {
                throw new Exception("Amount must be greater than 0");
            }
            if (item == null)
            {
                return null;
            }
            item.Amount += request.Amount;
            ItemRepository.Update(item);
            return item;
        }



    }
}
