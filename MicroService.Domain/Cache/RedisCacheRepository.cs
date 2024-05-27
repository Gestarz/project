using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MicroService.Domain.Cache
{
    public class RedisCacheRepository : ICacheRepository
    {
        private IDistributedCache _client;
        public RedisCacheRepository(IDistributedCache cacheClient)
        {
            _client = cacheClient;
        }

        private string Pack(object value) => JsonConvert.SerializeObject(value);
        private T Unpack<T>(string value) => JsonConvert.DeserializeObject<T>(value);

        public void Add<T>(string key, T value)
        {
            _client.SetString(key, Pack(value));
        }

        public T Get<T>(string key)
        {
            return Unpack<T>(_client.GetString(key));
        }

        public void Remove(string key)
        {
            _client.Remove(key);
        }
    }
}
