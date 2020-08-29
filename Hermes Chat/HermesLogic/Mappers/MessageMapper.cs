using HermesDataAccess.Table;
using HermesModels.Chat;
using System.Collections.Generic;

namespace HermesLogic.Mappers
{
    public static class MessageMapper
    {
        public static MessageModel ToMessage(this MESSAGES dbo)
        {
            if (dbo == null)
            {
                return null;
            }

            return new MessageModel()
            {
                Id = dbo.ID,
                UserName = dbo.USERNAME,
                Text = dbo.TEXT,
                CreationTime = dbo.CREATIONTIME,
                UserId = dbo.USERID
            };
        }

        public static List<MessageModel> ToMessageList(this IList<MESSAGES> dbo)
        {
            return dbo.ToGenericDtoList(ToMessage);
        }
    }
}