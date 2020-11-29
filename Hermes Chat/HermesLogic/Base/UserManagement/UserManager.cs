using HermesModels.User;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Base.UserManagement
{
    /// <summary>
    /// Individual user management, for current user.
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// Currently logged in user.
        /// </summary>
        public CurrentUserSessionState CurrentUserSessionValues { get; }

        /// <summary>
        /// Representation of current user.
        /// </summary>
        public ChatUser CurrentUser => _userNonManipulationLogic.GetUserById(CurrentUserSessionValues.AspNetUserId);

        /// <summary>
        /// Credentials management.
        /// </summary>
        public ICredentialManager CredentialsManager { get; }

        /// <summary>
        /// Non manipulative part of logic.
        /// </summary>
        private readonly IUserNonManipulativeLogic _userNonManipulationLogic;

        /// <summary>
        /// Individual user management, for current user.
        /// </summary>
        public UserManager(
            ICredentialManager credentialManager,
            IUserNonManipulativeLogic userNonManipulationLogic,
            IHttpContextAccessor httpContextAccessor)
        {
            CredentialsManager = credentialManager;
            _userNonManipulationLogic = userNonManipulationLogic;
            CurrentUserSessionValues = new CurrentUserSessionState(httpContextAccessor);
        }

        /// <summary>
        /// Gets users by it's ids.
        /// </summary>
        /// <param name="ids">Identifiers to find users by.</param>
        /// <returns>List of users.</returns>
        public Task<List<ChatUser>> GetUsersByIdsAsync(List<long> ids)
        {
            return _userNonManipulationLogic.GetUsersByIdsAsync(ids);
        }

        /// <summary>
        /// Gets user by it's id.
        /// </summary>
        /// <param name="id">Identifier to find user by.</param>
        /// <returns>User model.</returns>
        public ChatUser GetUserById(long id)
        {
            return _userNonManipulationLogic.GetUserById(id);
        }

        /// <summary>
        /// Checks if provided email exists.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if email exists, false otherwise.</returns>
        public bool IsEmailExisting(string email)
        {
            return _userNonManipulationLogic.IsUserWithSameEmailExisting(email);
        }

        /// <summary>
        /// Checks if provided username exists.
        /// </summary>
        /// <param name="username">Username to check.</param>
        /// <returns>True if username exists, false otherwise.</returns>
        public bool IsUsernameExisting(string username)
        {
            return _userNonManipulationLogic.IsUserWithSameUsernameExisting(username);
        }
    }
}