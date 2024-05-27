using MicroService.Domain.Class;
using MicroService.Shared.ApiClient;
using MicroService.Shared.Request.Item;
using MicroService.Shared.Request.Notify;
using MicroService.Shared.Request.User;
using MicroService.Shared.Response;

namespace MicroService.Gateway.Manager
{
    public class ProxyCallManager
    {
        private UserApiClientLoadManager UserApiClientLoadManager { get; }
        private ItemApiClient ItemApiClient { get; }
        private NotifyClient NotifyApiClient { get; }

        public ProxyCallManager(UserApiClientLoadManager userApiClientLoadManager, ItemApiClient itemApiClient, NotifyClient notifyApiClient)
        {
            UserApiClientLoadManager = userApiClientLoadManager;
            ItemApiClient = itemApiClient;
            NotifyApiClient = notifyApiClient;
        }

        public DetailedResponse<User> GetUser(GetUserRequest request)
        {
            return UserApiClientLoadManager.GetUser(request);
        }

        public Item GetItem(GetItemRequest request)
        {
            return ItemApiClient.GetItem(request);
        }

        // buy item
        public DetailedResponse<Transaction> BuyItem(BuyRequest request)
        {
            return UserApiClientLoadManager.BuyItem(request);
        }

        public bool TriggerBuyItem(BuyRequest request)
        {
            return ItemApiClient.BuyItem(request);
        }

        public void Notify(NotifyUserRequest request)
        {
            NotifyApiClient.NotifyUser(request);
        }


    }
}
