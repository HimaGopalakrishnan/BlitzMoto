using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;

namespace App.Providers.Cache.Services
{
    public class CacheService : ICacheService
    {
        IBlobCache cache = BlobCache.LocalMachine;

        public CacheService()
        {
        }

        public async Task<T> GetCachedObject<T>(string key)
        {
            return await cache.GetObject<T>(key);
        }

        public async Task SaveObjectInCache<T>(string key, T data)
        {
            await cache.InsertObject(key, data);
        }
    }
}
