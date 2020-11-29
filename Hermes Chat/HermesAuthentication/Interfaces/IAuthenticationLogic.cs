using HermesDataAccess.Interfaces;
using HermesModels.MVC;
using HermesModels.User;

namespace AuthenAuthorAccount.Interfaces
{
    /// <summary>
    /// Logic for authentication.
    /// </summary>
    public interface IAuthenticationLogic
    {
        /// <summary>
        /// User registration.
        /// </summary>
        /// <param name="registrationModel">Model to register values from.</param>
        void RegisterNewUser(RegistrationModel registrationModel);

        /// <summary>
        /// User log in.
        /// </summary>
        /// <param name="aspNetUser">User to log in.</param>
        void LoginUser(ChatUser aspNetUser);

        /// <summary>
        /// Logs out current user.
        /// </summary>
        /// <param name="aspNetUser">User to logout.</param>
        void LogoutUser(ChatUser aspNetUser);
    }
}