using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace ProMVC.WebFramework
{

    public interface ICacheProvider
    {
        T GetObject<T>(string key);
        void SetObject<T>(string key, T value);
    }

    public class IMemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache memoryCache;

        public IMemoryCacheProvider(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public T GetObject<T>(string key)
        {
            return memoryCache.Get<T>(key);
        }
        public void SetObject<T>(string key, T value)
        {
            memoryCache.Set<T>(key, value);
        }
    }
    public class DistributedCacheProvider : ICacheProvider
    {
        private readonly IDistributedCache distributedCache;

        public DistributedCacheProvider(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public T GetObject<T>(string key)
        {
           return distributedCache.GetString(key).ToModel<T>();
        }
        public void SetObject<T>(string key, T value)
        {
            distributedCache.SetString(key, value.ToJson());
        }
    }
}
