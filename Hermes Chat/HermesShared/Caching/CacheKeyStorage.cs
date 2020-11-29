using HermesModels.User;

namespace HermesShared.Caching
{
    /// <summary>
    /// Singleton, so it would be available in static configuration manager.
    /// </summary>
    public class CacheKeyStorage
    {
        private readonly string _sessionCacheKey = "_session";

        /// <summary>
        /// Cache for chat user, base model.
        /// </summary>
        private static readonly string _chatUserCacheKey = $"{nameof(ChatUser)}";

        /// <summary>
        /// Cache key for empty query.
        /// </summary>
        private static readonly string _emptyQueryCacheKey = "emptyQ";

        // Create unique instance
        private static readonly CacheKeyStorage _instance = new CacheKeyStorage();

        // Make constructor private to avoid instantiation from the outside
        private CacheKeyStorage() { }
        
        // Expose unique instance
        public static CacheKeyStorage Instance
        {
            get { return _instance; }
        }

        public string SessionCacheKeyForUser(string userId)
        {
            return _sessionCacheKey + userId;
        }

        public string SessionCacheKeyForUser(long userId)
        {
            return _sessionCacheKey + userId.ToString();
        }

        /// <summary>
        /// Gets cache key for chat user formatted with passed in user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Chat user key.</returns>
        public static string GetUserKey(long userId)
        {
            return string.Format("{0}:{1}", _chatUserCacheKey, userId);
        }

        /// <summary>
        /// Gets default cache key for queries without parameters (empty queries).
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Default cache key for empty queries.</returns>
        public static string GetParameterlessQueryKey()
        {
            return _emptyQueryCacheKey;
        }
    }
}