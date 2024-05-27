using MicroService.Domain.Class;
using MicroService.Shared.Request.User;
using MicroService.Shared.Response;
using Newtonsoft.Json;
using System.Text;

namespace MicroService.Shared.ApiClient
{
    public class UserApiClient
    {
        private HttpClient HttpClient { get; set; }

        public UserApiClient(string baseURL)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(baseURL);
        }

        public DetailedResponse<User> GetUser(GetUserRequest request)
        {
            var response = HttpClient.GetAsync($"User/Get/?Guid={request.Guid}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<DetailedResponse<User>>(content);
            }
            return null;
        }

        // buy item
        public DetailedResponse<Transaction> BuyItem(BuyRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = HttpClient.PostAsync("User/Buy", data).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<DetailedResponse<Transaction>>(content);
            }
            return null;
        }






    }
}
