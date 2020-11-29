using HermesModels.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Features.Chat.Interfaces
{
    /// <summary>
    /// Messaging system, for sending messages in chat/s.
    /// </summary>
    public interface IMessageLogic
    {
        /// <summary>
        /// Send message to global chat.
        /// </summary>
        /// <param name="message">Message model to send.</param>
        Task SendMessageAsync(MessageModel message);

        /// <summary>
        /// Returns all messages for global chat.
        /// </summary>
        Task<List<MessageModel>> GetAllMessagesAsync();
    }
}