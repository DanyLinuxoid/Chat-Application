using HermesModels.Chat;
using HermesModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Interfaces
{
    public interface IChatLogic
    {
        Task<ChatInformation> GetChatPreloadInformationAsync();

        Task<bool?> SendMessageAsync(MessageModel message);

        Task<List<AspNetUser>> GetAllLoggedUsersAsync();

        Task<List<MessageModel>> GetAllMessagesAsync();
    }
}