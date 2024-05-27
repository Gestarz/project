using MicroService.Domain.Class;
using MicroService.Shared.Request.Item;
using MicroService.Shared.Request.User;
using Newtonsoft.Json;
using System.Text;

namespace MicroService.Shared.ApiClient
{
    public class ItemApiClient
    {
        private HttpClient HttpClient { get; set; }

        public ItemApiClient(string baseURL)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(baseURL);
        }

        public Item GetItem(GetItemRequest request)
        {
            var response = HttpClient.GetAsync($"Item/Get/?Guid={request.Guid}").Result;
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
            var response = HttpClient.PostAsync("Item/Buy", data).Result;
            return response.IsSuccessStatusCode;
        }
    }
}
