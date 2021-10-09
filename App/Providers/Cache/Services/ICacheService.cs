using System.Threading.Tasks;

namespace App.Providers.Cache.Services
{
    public interface ICacheService
    {
        Task<T> GetCachedObject<T>(string key);
        Task SaveObjectInCache<T>(string key, T data);
    }
}
