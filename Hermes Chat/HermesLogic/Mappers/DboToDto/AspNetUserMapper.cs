using HermesDataAccess.Tables;
using HermesLogic.Base;
using HermesModels.User;
using System.Collections.Generic;

namespace HermesLogic.Mappers.DboToDto
{
    public static class AspNetUserMapper
    {
        public static AspNetUser ToAspNetUser(this ASPNET_USER dbo)
        {
            return new AspNetUser()
            {
                Id = dbo.ID,
                CreationTime = dbo.CREATION_TIME,
                IsLockoutEnabled = dbo.IS_LOCKOUT_ENABLED,
                AccessFailedCount = dbo.ACCESS_FAILED_COUNT,
                LockoutEnd = dbo.LOCKOUT_END,
                ModificationTime = dbo.MODIFICATION_TIME,
            };
        }

        public static List<AspNetUser> ToAspNetUserList(this IList<ASPNET_USER> dbo)
        {
            return dbo.ToGenericDtoList(ToAspNetUser);
        }
    }
}