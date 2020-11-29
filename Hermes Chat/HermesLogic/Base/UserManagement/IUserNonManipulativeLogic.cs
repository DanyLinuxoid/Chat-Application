using HermesModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Base.UserManagement
{
    /// <summary>
    /// User non manipulative logic, simply contains logic to retrieve information about users.
    /// </summary>
    public interface IUserNonManipulativeLogic
    {
        /// <summary>
        /// Gets users by it's ids.
        /// </summary>
        /// <param name="ids">Identifiers to find users by.</param>
        /// <returns>List of chat users.</returns>
        Task<List<ChatUser>> GetUsersByIdsAsync(List<long> ids);

        /// <summary>
        /// Gets user by identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>User model.</returns>
        ChatUser GetUserById(long userId);

        /// <summary>
        /// Checks is user with same email exists.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        bool IsUserWithSameEmailExisting(string email);

        /// <summary>
        /// Checks is user with username email exists.
        /// </summary>
        /// <param name="email">username to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        bool IsUserWithSameUsernameExisting(string username);
    }
}