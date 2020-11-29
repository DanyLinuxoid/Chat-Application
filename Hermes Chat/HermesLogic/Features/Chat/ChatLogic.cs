using HermesAuthentication.Session;
using HermesLogic.Base.UserManagement;
using HermesLogic.Features.Chat.Interfaces;
using HermesModels.Chat;
using HermesModels.MVC;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HermesLogic.Features.Chat
{
    /// <summary>
    /// Logic for chat itself, global or any other.
    /// </summary>
    public class ChatLogic : IChatLogic
    {
        /// <summary>
        /// Logic messaging, sending/getting etc.
        /// </summary>
        private readonly IMessageLogic _messageLogic;

        /// <summary>
        /// User management.
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// Logic for session and session values.
        /// </summary>
        private readonly ISessionLogic _sessionLogic;

        /// <summary>
        /// Logic for chat itself, global or any other.
        /// </summary>
        public ChatLogic(
            IMessageLogic messageLogic, 
            IUserManager userManager,
            ISessionLogic sessionLogic)
        {
            _messageLogic = messageLogic;
            _userManager = userManager;
            _sessionLogic = sessionLogic;
        }

        /// <summary>
        /// Gets chat preload information, users, messages...
        /// </summary>
        /// <returns>Object with chat information</returns>
        public async Task<ChatInformationViewModel> GetChatPreloadInformationAsync()
        {
            var messages = await _messageLogic.GetAllMessagesAsync();

            // Getting users online by user session identifiers.
            var loggedUsers = await _userManager.GetUsersByIdsAsync(_sessionLogic.GetActiveUsersSessionIds());
            var currentUserId = _userManager.CurrentUserSessionValues.AspNetUserId;

            // Remove current user from chat list.
            loggedUsers.Remove(loggedUsers.FirstOrDefault(s => s.AspNetUserId == currentUserId));
            var usersRelatedToMessages = await _userManager.GetUsersByIdsAsync(messages.Select(x => x.UserId).ToList());

            // Converting retrieved users to model with message and account profile image.
            List<MessageSenderModel> messageSenders = new List<MessageSenderModel>();
            messages.ForEach(x =>
                messageSenders.Add(new MessageSenderModel()
                {
                    MessageModel = x,
                    UserAccountImage = usersRelatedToMessages.First(y =>
                        y.AspNetUserId == x.UserId).AccountImage,
                }));

            // Fill view model.
            return new ChatInformationViewModel()
            {
                ChatSendersWithMessages = messageSenders,
                UsersOnline = loggedUsers,
                UserMessageToSendPlaceHolder = new MessageModel(),
            };
        }

        /// <summary>
        /// Delegates work to message logic.
        /// </summary>
        /// <param name="message">Message to send.</param>
        public async Task SendMessageAsync(MessageModel message)
        {
            await _messageLogic.SendMessageAsync(message);
        }
    }
}