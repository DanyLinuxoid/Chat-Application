using HermesModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Base.UserManagement
{
    /// <summary>
    /// Individual user management, for current user.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Currently logged in user.
        /// </summary>
        CurrentUserSessionState CurrentUserSessionValues { get; }

        /// <summary>
        /// Representation of current user.
        /// </summary>
        ChatUser CurrentUser { get; }

        /// <summary>
        /// Credentials management.
        /// </summary>
        ICredentialManager CredentialsManager { get; }

        /// <summary>
        /// Gets users by it's ids.
        /// </summary>
        /// <param name="ids">Identifiers to find users by.</param>
        /// <returns>List of users.</returns>
        Task<List<ChatUser>> GetUsersByIdsAsync(List<long> ids);

        /// <summary>
        /// Gets user by it's id.
        /// </summary>
        /// <param name="id">Identifier to find user by.</param>
        /// <returns>User model.</returns>
        ChatUser GetUserById(long id);

        /// <summary>
        /// Checks if provided username exists.
        /// </summary>
        /// <param name="username">Username to check.</param>
        /// <returns>True if username exists, false otherwise.</returns>
        bool IsUsernameExisting(string username);

        /// <summary>
        /// Checks if provided email exists.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if email exists, false otherwise.</returns>
        bool IsEmailExisting(string email);
    }
}