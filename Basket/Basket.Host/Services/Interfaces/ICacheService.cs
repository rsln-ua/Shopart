namespace Basket.Host.Services.Interfaces
{
    public interface ICacheService
    {
        Task Set<T>(string key, T value);

        Task<T> Get<T>(string key);
    }
}