using HermesDataAccess.Tables;
using HermesModels.User;
using System.Collections.Generic;

namespace HermesLogic.Mappers
{
    public static class AspNetUserMapper
    {
        public static AspNetUser ToAspNetUser(this ASPNET_USER dbo)
        {
            if (dbo == null)
            {
                return null;
            }

            return new AspNetUser()
            {
                Id = dbo.Id,
                Password = dbo.PasswordHash,
                UserName = dbo.UserName,
                Email = dbo.Email,
                CreationTime = dbo.CreationTime,
                IsEmailConfirmed = dbo.IsEmailConfirmed,
                PhoneNumber = dbo.PhoneNumber,
                IsPhoneNumberConfirmed = dbo.IsPhoneNumberConfirmed,
                IsTwoFactorEnabled = dbo.IsTwoFactorEnabled,
                LockoutEnabled = dbo.LockoutEnabled,
                AccessFailedCount = dbo.AccessFailedCount,
                IsLogged = dbo.IsLogged
            };
        }

        public static List<AspNetUser> ToAspNetUserList(this IList<ASPNET_USER> dbo)
        {
            return dbo.ToGenericDtoList(ToAspNetUser);
        }
    }
}