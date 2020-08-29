using HermesDataAccess.Interfaces;
using HermesLogic.Interfaces;
using HermesLogic.Mappers;
using HermesModels.User;
using HermesQueriesCommands.TBL_ASPNET_USER.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.UserInformation
{
    public class UserNonManipulativeLogic : IUserNonManipulativeLogic
    {
        private readonly ISqlDb _sqlDb;

        private readonly ICacheManager _memoryCache;
         
        public UserNonManipulativeLogic(ISqlDb sqlDb, ICacheManager memoryCache)
        {
            _sqlDb = sqlDb;
            _memoryCache = memoryCache;
        }

        public AspNetUser GetUserByUsername(string username)
        {
            if (username == null)
            {
                return null;
            }

            var value = _memoryCache.Get<AspNetUser>(username);
            if (value == null)
            {
                var user = _sqlDb.Query(new GetUserByUsernameQuery(username)).ToAspNetUser();
                if (user == null)
                {
                    return null;
                }

                _memoryCache.Set(username, user);
                return user;
            }

            return value;
        }

        public AspNetUser GetUserByEmail(string email)
        {
            return _sqlDb.Query(new GetUserByEmailQuery(email)).ToAspNetUser();
        }

        public bool IsUserWithSameEmailExisting(string email)
        {
            return GetUserByEmail(email) != null;
        }

        public bool IsUserWithSameUsernameExisting(string username)
        {
            return GetUserByUsername(username) != null;
        }

        public List<AspNetUser> GetAllLoggedUsers()
        {
            return _sqlDb.Query(new GetAllLoggedUsersQuery()).ToAspNetUserList();
        }

        public async Task<List<AspNetUser>> GetAllLoggedUsersAsync()
        {
            var result = await _sqlDb.QueryAsync(new GetAllLoggedUsersQuery());
            return result.ToAspNetUserList();
        }
    }
}