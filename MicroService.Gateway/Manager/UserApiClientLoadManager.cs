using MicroService.Domain.Class;
using MicroService.Shared.ApiClient;
using MicroService.Shared.Request.User;
using MicroService.Shared.Response;

namespace MicroService.Gateway.Manager
{
    public class UserApiClientLoadManager
    {
        private List<UserApiClient> userApiClients { get; set; }

        public UserApiClientLoadManager(List<UserApiClient> userApiClients)
        {
            this.userApiClients = userApiClients;
        }

        public DetailedResponse<User> GetUser(GetUserRequest request)
        {
            // random client
            var random = new Random();
            var index = random.Next(userApiClients.Count);
            var userApiClient = userApiClients[index];
            return userApiClient.GetUser(request);
        }

        // buy item 
        public DetailedResponse<Transaction> BuyItem(BuyRequest request)
        {
            // random client
            var random = new Random();
            var index = random.Next(userApiClients.Count);
            var userApiClient = userApiClients[index];
            return userApiClient.BuyItem(request);
        }
    }
}
