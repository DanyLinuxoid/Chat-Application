using HermesModels.MVC;

namespace HermesLogic.Features.Authentication.Interfaces
{
    /// <summary>
    /// Authentication web wrapper for logic in library.
    /// </summary>
    public interface IAuthenticationLogic
    {
        /// <summary>
        /// Delegates registration to authentication logic after password generation.
        /// </summary>
        /// <param name="registrationModel">Registration model from registration page.</param>
        void RegisterUser(RegistrationModel registrationModel);

        /// <summary>
        /// Retrives user from database by username and delegates logging in to authentication logic.
        /// </summary>
        /// <param name="loginModel">Login model from login page.</param>
        void LoginUser(LoginModel loginModel);

        /// <summary>
        /// Delegates work to authentication logic, that logs out user.
        /// </summary>
        /// <param name="userId">User identifier by which he should be logged out.</param>
        void LogoutUser();
    }
}