using HermesDataAccess.Tables;
using HermesLogic.Base;
using HermesModels.User;
using System.Collections.Generic;

namespace HermesLogic.Mappers.DboToDto
{
    public static class AccountDetailsMapper
    {
        public static AccountDetails ToAccountDetailsDto(this ACCOUNT_DETAILS dbo)
        {
            if (dbo is null)
            {
                return null;
            }

            return new AccountDetails()
            {
                Id = dbo.ID,
                Username = dbo.USERNAME,
                AboutMe = dbo.ABOUT_ME,
                AspnetUserId = dbo.ASPNET_USER_ID,
                Email = dbo.EMAIL,
                IsEmailConfirmed = dbo.IS_EMAIL_CONFIRMED,
                IsPhoneNumberConfirmed = dbo.IS_PHONE_NUMBER_CONFIRMED,
                IsTwoFactorEnabled = dbo.IS_TWO_FACTOR_ENABLED,
                ModificationTime = dbo.MODIFICATION_TIME,
                PasswordHash = dbo.PASSWORD_HASH,
                PhoneNumber = dbo.PHONE_NUMBER,
                ProfilePhotoId = dbo.PROFILE_PHOTO_ID.HasValue ? dbo.PROFILE_PHOTO_ID.Value : (long?)null,
            };
        }

        public static List<AccountDetails> ToAccountDetailsDtoList(this IList<ACCOUNT_DETAILS> dbo)
        {
            return dbo.ToGenericDtoList(ToAccountDetailsDto);
        }
    }
}
