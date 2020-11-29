using AuthenAuthorAccount.Interfaces;
using HermesAuthentication.Session;
using HermesDataAccess.Enums;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using HermesModels.MVC;
using HermesModels.User;
using HermesQueriesCommands.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace AuthenAuthorAccount.Authentication
{
    /// <summary>
    /// Authentication logic for user.
    /// </summary>
    public class AuthenticationLogic : IAuthenticationLogic
    {
        /// <summary>
        /// Cookies and all related logic.
        /// </summary>
        private readonly ICookieLogic _cookieLogic; 

        /// <summary>
        /// Http context access.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Session related logic.
        /// </summary>
        private readonly ISessionLogic _sessionLogic;

        /// <summary>
        /// Database instance.
        /// </summary>
        private readonly ISqlDb _sqlDb;

        /// <summary>
        /// Authentication logic for user.
        /// </summary>
        public AuthenticationLogic(
            IHttpContextAccessor httpContextAccessor,
            ICookieLogic cookieLogic,
            ISessionLogic sessionLogic,
            ISqlDb sqlDb)
        {
            _cookieLogic = cookieLogic;
            _httpContextAccessor = httpContextAccessor;
            _sessionLogic = sessionLogic;
            _sqlDb = sqlDb;
        }

        /// <summary>
        /// Registers new user by creating new asp net user related account information.
        /// </summary>
        /// <param name="registrationModel">Registration model to use.</param>
        public void RegisterNewUser(RegistrationModel registrationModel)
        {
            var aspnetUser = _sqlDb.Command(new ASPNET_USERS_Commands(), CommandTypes.Create, new ASPNET_USER());

            ACCOUNT_DETAILS accountDetails = new ACCOUNT_DETAILS()
            {
                USERNAME = registrationModel.UserName,
                EMAIL = registrationModel.Email,
                PASSWORD_HASH = registrationModel.Password,
                ASPNET_USER_ID = aspnetUser.Id,
                PROFILE_PHOTO_ID = 1, // Default picture for all new users.
            };
            _sqlDb.Command(new ACCOUNT_DETAILS_Commands(), CommandTypes.Create, accountDetails);
        }

        /// <summary>
        /// Logs in user.
        /// </summary>
        /// <param name="aspNetUser">Asp net user to log in.</param>
        public void LoginUser(ChatUser aspNetUser)
        {
            // Setting cookies for user.
            var cookies = _cookieLogic.GetDefaultCookieOptions(aspNetUser.Username);
            _httpContextAccessor.HttpContext.SignInAsync(cookies.ClaimsPrincipal, cookies.AuthenticationProperties);

            // Setting session for user.
            _sessionLogic.SetSessionValuesForUser(aspNetUser);
        }

        /// <summary>
        /// Logs out passed in user.
        /// </summary>
        /// <param name="aspNetUser">User to log out.</param>
        public void LogoutUser(ChatUser aspNetUser)
        {
            // Clear cookies.
            _httpContextAccessor.HttpContext.SignOutAsync();

            // Clear session.
            _sessionLogic.ClearSessionForUser(aspNetUser);
        }
    }
}