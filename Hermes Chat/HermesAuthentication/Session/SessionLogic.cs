using HermesModels.User;
using HermesShared.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HermesAuthentication.Session
{
    /// <summary>
    /// Session management.
    /// </summary>
    public class SessionLogic : ISessionLogic
    {
        /// <summary>
        /// Http context, session is available through it.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Session key for asp net user identifier.
        /// </summary>
        private const string _sessionUserIdKey = "AspNetUserId";

        /// <summary>
        /// Dictionary of active sessions.
        /// </summary>
        public ConcurrentDictionary<long, DateTime> Sessions { get; private set; } = new ConcurrentDictionary<long, DateTime>();

        /// <summary>
        /// Session management.
        /// </summary>
        public SessionLogic(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Set user values for session to retrieve later.
        /// </summary>
        /// <param name="user">User to set values for.</param>
        public void SetSessionValuesForUser(ChatUser user)
        {
            _httpContextAccessor.HttpContext.Session.SetString(_sessionUserIdKey, user.AspNetUserId.ToString());

            // Adding user id to session list
            Sessions.TryAdd(user.AspNetUserId, DateTime.Now.AddMinutes(ConfigurationManager.DefaultUserSessionExpirationTimeInMinutes));
        }

        /// <summary>
        /// Clears session for passed in user.
        /// </summary>
        /// <param name="user">User to clear session for.</param>
        public void ClearSessionForUser(ChatUser user)
        {
            // Removing user id from active sessions.
            Sessions.TryRemove(user.AspNetUserId, out _);

            _httpContextAccessor.HttpContext.Session.Clear();

            // Launch clean up task in background if too much sessions.
            if (Sessions.Count > 2000)
            {
                Task.Run(() => RunCleaningTask());
            }
        }

        /// <summary>
        /// Gets all non-expired uses sessions in list.
        /// </summary>
        /// <returns>Active session identifiers.</returns>
        public List<long> GetActiveUsersSessionIds()
        {
            return Sessions
                .Where(m => m.Value > DateTime.Now)
                .Select(m => m.Key)
                .ToList();
        }

        /// <summary>
        /// Clean up expired sessions.
        /// This can be done in some scheduler or whatever...
        /// </summary>
        private void RunCleaningTask()
        {
            var sessionsToClear = Sessions.Where(m => m.Value < DateTime.Now);
            foreach (var session in sessionsToClear)
            {
                Sessions.TryRemove(session.Key, out DateTime _);
            }
        }
    }
}