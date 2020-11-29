using HermesModels.Chat;
using System.Threading.Tasks;

namespace HermesLogic.Features.Chat.Interfaces
{
    /// <summary>
    /// Logic for chat itself, global or any other.
    /// </summary>
    public interface IChatLogic
    {
        /// <summary>
        /// Gets chat preload information, users, messages...
        /// </summary>
        /// <returns>Object with chat information</returns>
        Task<ChatInformationViewModel> GetChatPreloadInformationAsync();

        /// <summary>
        /// Delegates work to message logic.
        /// </summary>
        /// <param name="message">Message to send.</param>
        Task SendMessageAsync(MessageModel message);
    }
}