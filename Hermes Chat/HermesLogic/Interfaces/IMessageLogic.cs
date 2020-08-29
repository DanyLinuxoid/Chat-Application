using HermesModels.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Interfaces
{
    public interface IMessageLogic
    {
        Task <bool?> SendMessageAsync(MessageModel message);

        Task <List<MessageModel>> GetAllMessagesAsync();
    }
}