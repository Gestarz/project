namespace MicroService.Domain.Cache
{
    public interface ICacheRepository
    {
        void Add<T>(string key, T value);
        T Get<T>(string key);
        void Remove(string key);
    }
}
