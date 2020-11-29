using System;

namespace HermesShared.Caching
{
    /// <summary>
    /// Caching for queries functionality.
    /// </summary>
    public class CacheNQuery : ICacheNQuery
    {
        /// <summary>
        /// Core caching functionality.
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Caching for queries functionality.
        /// </summary>
        public CacheNQuery(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Functionality to cache query and to retrieve it values from cache.
        /// NOTE: Cache invalidation should be done on upper levels!
        /// </summary>
        /// <typeparam name="T">Type of object (query).</typeparam>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="queryCallback">Callback (query) to execute if no value found in cache.</param>
        /// <returns>Object from cache if found OR object from query.</returns>
        public T CacheNExecute<T>(string cacheKey, Func<T> queryCallback)
        {
            T result = _cacheManager.Get<T>(cacheKey);
            if (result == null)
            {
                result = queryCallback();
                _cacheManager.Set(cacheKey, result);
            }

            return result;
        }
    }
}
