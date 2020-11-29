using HermesModels.User;

namespace HermesLogic.Base
{
    /// <summary>
    /// Cache keys for caching.
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// Cache for chat user, base model.
        /// </summary>
        private static readonly string _chatUserCacheKey = $"{nameof(ChatUser)}";

        /// <summary>
        /// Cache key for empty query.
        /// </summary>
        private static readonly string _emptyQueryCacheKey = "emptyQ";

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
