using HermesModels.MVC;
using System.Threading.Tasks;

namespace HermesLogic.Features.AccountManagement.Interfaces
{
    /// <summary>
    /// Account management logic, where user can modify his details.
    /// </summary>
    public interface IAccountManagementLogic
    {
        /// <summary>
        /// Account management logic, where user can modify his details.
        /// </summary>
        Task UpdateUserAccountDetails(UserProfileModel model);

        /// <summary>
        /// Gets user profile model by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>User profile model with account details.</returns>
        Task<UserProfileModel> GetUserProfileModelByUserIdAsync(long userId);
    }
}
