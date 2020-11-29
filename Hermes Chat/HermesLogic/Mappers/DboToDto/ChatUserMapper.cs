using HermesDataAccess.Tables;
using HermesLogic.Base;
using HermesModels.User;
using System.Collections.Generic;

namespace HermesLogic.Mappers.DboToDto
{
    public static class ChatUserMapper
    {
        public static ChatUser ToChatUser(this ACCOUNT_DETAILS dbo)
        {
            return new ChatUser()
            {
                AspNetUserId = dbo.ASPNET_USER_ID,
                Username = dbo.USERNAME,
                AccountImageId = dbo.PROFILE_PHOTO_ID,
                Email = dbo.EMAIL,
            };
        }

        public static List<ChatUser> ToChatUserList(this IList<ACCOUNT_DETAILS> dbo)
        {
            return dbo.ToGenericDtoList(ToChatUser);
        }
    }
}