using Microsoft.AspNetCore.Http;
using System;

namespace HermesModels.User
{
    /// <summary>
    /// Represents current user session state, where user values are stored.
    /// </summary>
    public class CurrentUserSessionState
    {
        /// <summary>
        /// Http context accessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Represents current user session state, where user values are stored.
        /// </summary>
        public CurrentUserSessionState(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Asp net user identifier.
        /// </summary>
        public long AspNetUserId
        {
            get
            {
                return this.GetValueCallback("AspNetUserId", value => long.Parse(System.Text.Encoding.UTF8.GetString(value)));
            }
        }

        /// <summary>
        /// Generic method to retrieve any value from user session (if it exists and is defined.)
        /// </summary>
        /// <typeparam name="T">Type of value to retrieve.</typeparam>
        /// <param name="fieldName">Name of field to retrieve.</param>
        /// <param name="callback">Callback method that should be executed when value is found.</param>
        /// <returns>Session value that was requested.</returns>
        private T GetValueCallback<T>(string fieldName, Func<byte[], T> callback)
        {
            var valueExists = _httpContextAccessor.HttpContext.Session.TryGetValue(fieldName, out byte[] value);
            return valueExists && value != null
                ? callback(value)
                : throw new InvalidOperationException($"{fieldName} retrieval from session failed");
        }
    }
}
