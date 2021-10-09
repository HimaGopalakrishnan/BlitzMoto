using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Providers.Cache.Services
{
    public class MockCacheService : ICacheService
    {
        Dictionary<string, object> KeyValuePairs;

        public MockCacheService()
        {
            KeyValuePairs = new Dictionary<string, object>();
        }

        public async Task<T> GetCachedObject<T>(string key)
        {
            await Task.Delay(1);
            if (KeyValuePairs.ContainsKey(key))
            {
                return (T)KeyValuePairs[key];
            }
            throw new KeyNotFoundException();
        }

        public async Task SaveObjectInCache<T>(string key, T data)
        {
            await Task.Delay(1);
            KeyValuePairs.Add(key, data);
        }
    }
}
