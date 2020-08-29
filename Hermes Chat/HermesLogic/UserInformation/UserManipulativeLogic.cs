using HermesDataAccess.Enums;
using HermesDataAccess.Interfaces;
using HermesLogic.Interfaces;
using HermesModels.User;
using HermesQueriesCommands.TBL_ASPNET_USER;
using System.Collections.Generic;

namespace HermesLogic.UserInformation
{
    public class UserManipulativeLogic : IUserManipulationLogic
    {
        private readonly ISqlDb _sqlDb;
        private readonly ICacheManager _memoryCache;

        public UserManipulativeLogic(ISqlDb sqlDb, ICacheManager memoryCache)
        {
            _sqlDb = sqlDb;
            _memoryCache = memoryCache;
        }

        public void DeleteUser(string username)
        {
            _sqlDb.Command(new ASPNET_USER_Commands(), CommandTypes.Delete, new Dictionary<string, object>()
            {
                ["Username"] = username, 
            });

            //_memoryCache.Remove(domainName);
        }

        public void UpdateUserInformation(AspNetUser user, Dictionary<string, object> parameters)
        {
            _sqlDb.Command(new ASPNET_USER_Commands(), CommandTypes.Update, parameters);
            //_memoryCache.Remove(user.DomainName);
            //_memoryCache.Set(user.DomainName, user); /// to base class
        }
    }
}