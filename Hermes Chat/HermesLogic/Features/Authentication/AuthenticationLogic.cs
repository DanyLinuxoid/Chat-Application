using HermesDataAccess.Interfaces;
using HermesLogic.Base.UserManagement;
using HermesLogic.Mappers.DboToDto;
using HermesModels.MVC;
using HermesModels.User;
using HermesQueriesCommands.Queries;

namespace HermesLogic.Features.Authentication
{
    /// <summary>
    /// Authentication web wrapper for logic in library.
    /// </summary>
    public class AuthenticationLogic : Interfaces.IAuthenticationLogic
    {
        /// <summary>
        /// Authentication logic in library class.
        /// </summary>
        private readonly AuthenAuthorAccount.Interfaces.IAuthenticationLogic _authenticationLogic;

        /// <summary>
        /// User management.
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// User management.
        /// </summary>
        private readonly ISqlDb _sqlDb;

        /// <summary>
        /// Web wrapper for logic in library.
        /// </summary>
        public AuthenticationLogic(
            AuthenAuthorAccount.Interfaces.IAuthenticationLogic authenticationLogic,
            IUserManager userManager,
            ISqlDb sqlDb)
        {
            _authenticationLogic = authenticationLogic;
            _userManager = userManager;
            _sqlDb = sqlDb;
        }

        /// <summary>
        /// Retrives user from database by username and delegates logging in to authentication logic.
        /// </summary>
        /// <param name="loginModel">Login model from login page.</param>
        public void LoginUser(LoginModel loginModel)
        {
            ChatUser user = _sqlDb.CacheNQuery(new GetUserDetailsByUsernameQuery(loginModel.Username), loginModel.Username).ToChatUser();
           _authenticationLogic.LoginUser(user);
        }

        /// <summary>
        /// Delegates registration to authentication logic after password generation.
        /// </summary>
        /// <param name="registrationModel">Registration model from registration page.</param>
        public void RegisterUser(RegistrationModel registrationModel) // errors can be catched in action filter and can be returned to previous page or smth like that, no need for bool and checks in controller...
        {
            registrationModel.Password = _userManager.CredentialsManager.GetUserPasswordInHashedFormat(registrationModel.Password);
            _authenticationLogic.RegisterNewUser(registrationModel);
        }

        /// <summary>
        /// Delegates work to authentication logic, that logs out user.
        /// </summary>
        /// <param name="userId">User identifier by which he should be logged out.</param>
        public void LogoutUser()
        {
            _authenticationLogic.LogoutUser(_userManager.CurrentUser);
        }
    }
}