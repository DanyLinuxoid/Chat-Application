using HermesLogic.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace HermesLogic.Caching
{
    public class CacheManager : ICacheManager 
    {
        private readonly IMemoryCache _memoryCache;

        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Update(string cacheKey, object value, DateTimeOffset? time = null)
        {
            Remove(cacheKey);
            Set(cacheKey, value, time);
        }

        public T Get<T>(string cacheKey)
        {
            _memoryCache.TryGetValue(cacheKey, out T result);
            return result;
        }

        public void Set(string cacheKey, object value, DateTimeOffset? time = null)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"{ value } was null.");
            }

            _memoryCache.Set(cacheKey, value, time.HasValue ? time.Value : GetDefaultExpirationTime());
        }

        public void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

        public bool Contains(string cacheKey)
        {
            return _memoryCache.TryGetValue(cacheKey, out object value);
        }

        private DateTimeOffset GetDefaultExpirationTime()
        {
            // Default cache time - 60 minutes, same as session length
            return new DateTimeOffset(DateTime.Now.AddMinutes(60));
        }
    }
}