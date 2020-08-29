using System;

namespace HermesLogic.Interfaces
{
    public interface ICacheManager
    {
        T Get<T>(string cacheKey);

        void Set(string cacheKey, object value, DateTimeOffset? time = null);

        void Update(string cacheKey, object value, DateTimeOffset? time = null);

        void Remove(string cacheKey);

        bool Contains(string cacheKey);
    }
}