using HermesDataAccess.Tables;
using HermesModels.MVC;

namespace HermesLogic.Mappers.DtoToDbo
{
    /// <summary>
    /// Mapper for account details.
    /// </summary>
    public static class AccountDetailsMapper
    {
        /// <summary>
        /// Maps data transfer object model to database transfer object.
        /// </summary>
        /// <param name="dto">Datatransfer object.</param>
        /// <returns>Mapped database object representation.</returns>
        public static ACCOUNT_DETAILS ToAccountDetailsDbo(this UserProfileModel dto)
        {
            return new ACCOUNT_DETAILS()
            {
                ID = dto.Id,
                ASPNET_USER_ID = dto.AspNetUserId,
                USERNAME = dto.UserName,
                ABOUT_ME = dto.AboutMe,
                EMAIL = dto.Email,
                PHONE_NUMBER = dto.PhoneNumber,
                PROFILE_PHOTO_ID = dto.AccountImage?.FileId,
            };
        }
    }
}
