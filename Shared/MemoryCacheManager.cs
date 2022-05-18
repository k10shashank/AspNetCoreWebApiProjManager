using System;
using System.Runtime.Caching;

namespace AspNetCoreWebApiProjManager.Shared
{
    public static class MemoryCacheManager
    {
        public static void AddCacheEntry(string key, object value, TimeSpan? expirationTime = null)
        {
            if (expirationTime is null)
                expirationTime = new TimeSpan(2, 0, 0);
            ObjectCache cache = MemoryCache.Default;
            cache.Add(new CacheItem(key, value), new CacheItemPolicy() { SlidingExpiration = expirationTime.Value });
        }

        public static void RemoveCacheEntry(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            cache.Remove(key);
        }

        public static object GetCacheData(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItem item = cache.GetCacheItem(key);

            return item?.Value;
        }
    }
}
