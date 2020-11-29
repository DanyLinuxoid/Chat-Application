using HermesDataAccess.Interfaces;
using HermesLogic.Mappers.DboToDto;
using HermesModels.Base;
using HermesModels.User;
using HermesQueriesCommands.Queries;
using HermesShared.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HermesLogic.Base.UserManagement
{
    /// <summary>
    /// User non manipulative logic, simply contains logic to retrieve information about users.
    /// </summary>
    public class UserNonManipulativeLogic : IUserNonManipulativeLogic 
    {
        /// <summary>
        /// Database connection.
        /// </summary>
        private readonly ISqlDb _sqlDb;

        /// <summary>
        /// User non manipulative logic, simply contains logic to retrieve information about users.
        /// </summary>
        public UserNonManipulativeLogic(ISqlDb sqlDb)
        {
            _sqlDb = sqlDb;
        }

        /// <summary>
        /// Gets user by identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>User model.</returns>
        public ChatUser GetUserById(long userId)
        {
            var user = _sqlDb.CacheNQuery(new GetUserDetailsByUserIdQuery(userId), userId);
            var userProfileImage = _sqlDb.CacheNQuery(new FilePhotoGetByIdQuery(user.PROFILE_PHOTO_ID.Value), userId);
            var dtoUser = user.ToChatUser();
            dtoUser.AccountImage = userProfileImage.ToFileBase();
            return dtoUser;
        }

        /// <summary>
        /// Gets users by it's ids.
        /// </summary>
        /// <param name="ids">Identifiers to find users by.</param>
        /// <returns>List of users.</returns>
        public async Task<List<ChatUser>> GetUsersByIdsAsync(List<long> ids)
        {
            var users = await _sqlDb.QueryAsync(new GetUserDetailsByUserIdsQuery(ids));
            var chatUsers = users.ToChatUserList();
            var userAccountImages = await _sqlDb.QueryAsync(
                new AccountImagesGetByAspNetUserIdsQuery(
                    chatUsers.Select(x => x.AspNetUserId).ToList()));

            var userAccountImagesDto = userAccountImages.ToFileBase();
            Dictionary<long, FileBase> map = userAccountImagesDto.ToDictionary(x => x.FileId.Value, x => x);
            foreach (var user in chatUsers)
            {
                user.AccountImage = map[user.AccountImageId.Value];
            }

            return chatUsers;
        }

        /// <summary>
        /// Checks is user with same email exists.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        public bool IsUserWithSameEmailExisting(string email)
        {
            return _sqlDb.CacheNQuery(new IsUserExistingWithSameEmailQuery(email), email);
        }

        /// <summary>
        /// Checks is user with username email exists.
        /// </summary>
        /// <param name="username">username to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        public bool IsUserWithSameUsernameExisting(string username)
        {
            return _sqlDb.CacheNQuery(new IsUserExistingWithSameUsernameQuery(username), username);
        }
    }
}