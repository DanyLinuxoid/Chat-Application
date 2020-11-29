using HermesDataAccess.Tables;
using HermesLogic.Base;
using HermesModels.Chat;
using System.Collections.Generic;

namespace HermesLogic.Mappers.DboToDto
{
    public static class MessageMapper
    {
        public static MessageModel ToMessage(this MESSAGES dbo)
        {
            return new MessageModel()
            {
                Id = dbo.ID,
                UserName = dbo.USERNAME,
                Text = dbo.TEXT,
                CreationTime = dbo.CREATION_TIME,
                UserId = dbo.ASPNET_USER_ID,
            };
        }

        public static List<MessageModel> ToMessageList(this IList<MESSAGES> dbo)
        {
            return dbo.ToGenericDtoList(ToMessage);
        }
    }
}