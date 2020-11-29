using HermesDataAccess.Tables;
using HermesModels.Chat;

namespace HermesLogic.Mappers.DtoToDbo
{
    public static class MessageMapper
    {
        public static MESSAGES ToDbMessage(this MessageModel model)
        {
            return new MESSAGES()
            {
                ID = model.Id,
                ASPNET_USER_ID = model.UserId,
                CREATION_TIME = model.CreationTime,
                TEXT = model.Text,
                USERNAME = model.UserName,
            };
        }
    }
}
