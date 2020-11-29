using HermesDataAccess.Enums;
using HermesLogic.Features.AccountManagement.Interfaces;
using HermesModels.Base;
using HermesModels.MVC;
using HermesQueriesCommands.Commands;
using HermesQueriesCommands.Queries;
using HermesShared.Mappers;
using HermesLogic.Mappers.DtoToDbo;
using System.Threading.Tasks;
using HermesLogic.Files;
using HermesDataAccess.Interfaces;

namespace HermesLogic.Features.AccountManagement
{
    /// <summary>
    /// Account management logic, where user can modify his details.
    /// </summary>
    public class AccountManagementLogic : IAccountManagementLogic 
    {
        /// <summary>
        /// File download/upload logic.
        /// </summary>
        private readonly IFileLogic _fileLogic;

        /// <summary>
        /// File download/upload logic.
        /// </summary>
        private readonly ISqlDb _sqlDb;

        public AccountManagementLogic(
            IFileLogic fileLogic,
            ISqlDb sqldb) 
        {
            _fileLogic = fileLogic;
            _sqlDb = sqldb;
        }

        /// <summary>
        /// Account management logic, where user can modify his details.
        /// </summary>
        public async Task UpdateUserAccountDetails(UserProfileModel model)
        {
            model.Id = (await _sqlDb.QueryAsync(new GetUserDetailsByUserIdQuery(model.AspNetUserId))).ID;
            if (model.UploadedFiles.Count > 0)
            {
                var accountImage = model.UploadedFiles[0].ToFileBase();
                accountImage.UserId = model.AspNetUserId;
                accountImage.IsAccountImage = true;
                model.AccountImage = new FileBase()
                {
                    FileId = _fileLogic.UploadFile(accountImage),
                };
            }

            // Update user account details.
            _sqlDb.Command(new ACCOUNT_DETAILS_Commands(), CommandTypes.Update, model.ToAccountDetailsDbo());
        }

        /// <summary>
        /// Gets user profile model by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>User profile model with account details.</returns>
        public async Task<UserProfileModel> GetUserProfileModelByUserIdAsync(long userId)
        {
            var details = await _sqlDb.CacheNQueryAsync(new GetUserDetailsByUserIdQuery(userId), userId);
            var profileImage = _sqlDb.CacheNQueryAsync(new FilePhotoGetByIdQuery(details.PROFILE_PHOTO_ID.Value), details.PROFILE_PHOTO_ID.Value);

            return new UserProfileModel()
            {
                AspNetUserId = userId,
                UserName = details.USERNAME,
                Email = details.EMAIL,
                PhoneNumber = details.PHONE_NUMBER,
                AboutMe = details.ABOUT_ME,
                AccountImage = (await profileImage).ToFileBase(),
            };
        }
    }
}