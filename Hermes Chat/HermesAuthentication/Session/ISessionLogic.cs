using HermesModels.User;
using System.Collections.Generic;

namespace HermesAuthentication.Session
{
    /// <summary>
    /// Session management.
    /// </summary>
    public interface ISessionLogic
    {
        /// <summary>
        /// Set user values for session to retrieve later.
        /// </summary>
        /// <param name="user">User to set values for.</param>
        void SetSessionValuesForUser(ChatUser user);

        /// <summary>
        /// Clears session for passed in user.
        /// </summary>
        /// <param name="user">User to clear session for.</param>
        void ClearSessionForUser(ChatUser user);

        /// <summary>
        /// Gets all non-expired uses sessions in list.
        /// </summary>
        /// <returns>Active session identifiers.</returns>
        List<long> GetActiveUsersSessionIds();
    }
}