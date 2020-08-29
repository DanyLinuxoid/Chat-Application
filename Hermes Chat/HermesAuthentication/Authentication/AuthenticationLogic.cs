using AuthenAuthorAccount.Interfaces;
using HermesDataAccess.Enums;
using HermesDataAccess.Interfaces;
using HermesModels.MVC;
using HermesModels.User;
using HermesQueriesCommands.TBL_ASPNET_USER;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace AuthenAuthorAccount.Authentication
{
    public class AuthenticationLogic : IAuthenticationLogic
    {
        private readonly IMemoryCache _memoryCache; 

        private readonly ICookieLogic _cookieLogic; 

        private readonly ISqlDb _sqlDb;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationLogic(
            ISqlDb sqlDb, 
            IMemoryCache memoryCache, 
            ICookieLogic cookieLogic,
            IHttpContextAccessor httpContextAccessor)
        {
            _memoryCache = memoryCache;
            _sqlDb = sqlDb;
            _cookieLogic = cookieLogic;
            _httpContextAccessor = httpContextAccessor;
        }

        public IExecutionResult RegisterNewUser(RegistrationModel registrationModel)
        {
            return _sqlDb.Command(new ASPNET_USER_Commands(), CommandTypes.Create, new Dictionary<string, object>()
            {
                ["UserName"] = registrationModel.UserName,
                ["Email"] = registrationModel.Email,
                ["PasswordHash"] = registrationModel.Password,
            });
        }

        public IExecutionResult LoginUser(AspNetUser aspNetUser)
        {
             IExecutionResult result = _sqlDb.Command(new ASPNET_USER_Commands(), CommandTypes.Update, new Dictionary<string, object>
            {
                ["Id"] = aspNetUser.Id,
                ["IsLogged"] = 1,
            });

            if (result.Id != 0)
            {
                _memoryCache.Set(aspNetUser.Id, aspNetUser); // to base

                var cookies = _cookieLogic.GetDefaultCookieOptions(aspNetUser.UserName);
                _httpContextAccessor.HttpContext.SignInAsync(cookies.ClaimsPrincipal, cookies.AuthenticationProperties); // aye, make everything async + task 
            }

            return result;
        }

        public IExecutionResult LogoutUser(AspNetUser aspNetUser)
        {
             IExecutionResult result = _sqlDb.Command(new ASPNET_USER_Commands(), CommandTypes.Update, new Dictionary<string, object>
            {
                ["Id"] = aspNetUser.Id,
                ["IsLogged"] = 0,
            });

            if (result.Id != 0)
            {
                _memoryCache.Remove(aspNetUser.Id); // to base
                _httpContextAccessor.HttpContext.SignOutAsync(); // aye, make everything async + task 
            }

            return result;
        }
    }
}