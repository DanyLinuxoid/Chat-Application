using HermesShared.Configuration;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace HermesShared.Caching
{
    /// <summary>
    /// Cache management.
    /// </summary>
    public class CacheManager : ICacheManager 
    {
        /// <summary>
        /// Core cache instance.
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Cache management.
        /// </summary>
        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Updates cache record by provided cache key..
        /// </summary>
        /// <param name="cacheKey">Cache key to locate record.</param>
        /// <param name="value">Value to update to.</param>
        /// <param name="time">Time of cache entry to expire.</param>
        public void Update(string cacheKey, object value, DateTimeOffset? time = null)
        {
            Remove(cacheKey);
            Set(cacheKey, value, time);
        }

        /// <summary>
        /// Gets cache value by provided cache key.
        /// </summary>
        /// <typeparam name="T">Type of cache object.</typeparam>
        /// <param name="cacheKey">Cache key of required object.</param>
        /// <returns>Object from cache or null if not found.</returns>
        public T Get<T>(string cacheKey)
        {
            _memoryCache.TryGetValue(cacheKey, out T result);
            return result;
        }

        /// <summary>
        /// Sets value with provided cache key in cache.
        /// </summary>
        /// <param name="cacheKey">Key of record in cache.</param>
        /// <param name="value">Value that will reside with key.</param>
        /// <param name="time">Expiration time.</param>
        public void Set(string cacheKey, object value, DateTimeOffset? time = null)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"{ value } was null.");
            }

            _memoryCache.Set(cacheKey, value, time.HasValue 
                ? time.Value 
                : new DateTimeOffset(DateTime.Now.AddMinutes(ConfigurationManager.DefaultUserSessionExpirationTimeInMinutes)));
        }

        /// <summary>
        /// Tries to get value from cache by specified key, if not found - does callback (query) and sets result with specified cachekey.
        /// </summary>
        /// <param name="cacheKey">Key of record in cache.</param>
        /// <param name="time">Expiration time.</param>
        public object CacheNQuery<T>(string cacheKey, Func<T> callBack)
        {
            _memoryCache.TryGetValue(cacheKey, out object result);
            if (result == null)
            {
                result = callBack();
                _memoryCache.Set(cacheKey, result, new DateTimeOffset(DateTime.Now.AddMinutes(ConfigurationManager.DefaultUserSessionExpirationTimeInMinutes)));
            }

            return result;
        }

        /// <summary>
        /// Removes object from cache by specified key.
        /// </summary>
        /// <param name="cacheKey">Cache key to remove object by.</param>
        public void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

        /// <summary>
        /// Determines if cache contains object by specified cache key.
        /// </summary>
        /// <param name="cacheKey">Key to check object by.</param>
        /// <returns>True if object exists, false otherwise.</returns>
        public bool Contains(string cacheKey)
        {
            return _memoryCache.TryGetValue(cacheKey, out object _);
        }
    }
}