using MicroService.Domain.Class;
using MicroService.Shared.Request.Item;
using MicroService.Shared.Request.Notify;
using MicroService.Shared.Request.User;
using Newtonsoft.Json;
using System.Text;

namespace MicroService.Shared.ApiClient
{
    public class GatewayApiClient
    {
        private HttpClient HttpClient { get; set; }

        public GatewayApiClient(string baseURL)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(baseURL);
        }

        public Item GetItem(GetItemRequest request)
        {
            var response = HttpClient.GetAsync($"Gateway/GetItem/?Guid={request.Guid}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Item>(content);
            }
            return null;
        }

        public bool BuyItem(BuyRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = HttpClient.PostAsync("Gateway/BuyItem", data).Result;
            return response.IsSuccessStatusCode;
        }


        public bool TriggerBuyItem(BuyRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = HttpClient.PostAsync("Gateway/TriggerBuyItem", data).Result;
            return response.IsSuccessStatusCode;
        }

        public User GetUser(GetUserRequest request)
        {
            var response = HttpClient.GetAsync($"Gateway/GetUser/?Guid={request.Guid}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<User>(content);
            }
            return null;
        }

        public void NotifyUser(NotifyUserRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = HttpClient.PostAsync("Gateway/Notify", data).Result;
        }


    }
}
