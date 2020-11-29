using System;

namespace HermesShared.Caching
{
    /// <summary>
    /// Caching for queries functionality.
    /// </summary>
    public interface ICacheNQuery
    {
        /// <summary>
        /// Functionality to cache query and to retrieve it values from cache.
        /// NOTE: Cache invalidation should be done on upper levels!
        /// </summary>
        /// <typeparam name="T">Type of object (query).</typeparam>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="callback">Callback (query) to execute if no value found in cache.</param>
        /// <returns>Object from cache if found OR object from query.</returns>
        T CacheNExecute<T>(string cacheKey, Func<T> callback);
    }
}
