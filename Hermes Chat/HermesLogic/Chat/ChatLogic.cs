using HermesLogic.Base;
using HermesLogic.Interfaces;
using HermesModels.Chat;
using HermesModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesChat.HermesLogic.ChatLogic
{
    public class ChatLogic : BaseLogic, IChatLogic
    {
        private readonly IMessageLogic _messageLogic;

        public ChatLogic(
            IMessageLogic messageLogic, 
            ICommonDependencies commonDependencies)
            : base(commonDependencies)
        {
            _messageLogic = messageLogic;
        }

        public async Task<ChatInformation> GetChatPreloadInformationAsync()
        {
            var messages = await GetAllMessagesAsync();
            var loggedUsers = await GetAllLoggedUsersAsync();
            return new ChatInformation()
            {
                Messages = messages,
                UsersOnline = loggedUsers,
                UserMessage = new MessageModel(),
            };
        }

        public async Task<bool?> SendMessageAsync(MessageModel message)
        {
            if (!Validate(message))
            {
                return false;
            }

            return await _messageLogic.SendMessageAsync(message);
        }

        public async Task<List<AspNetUser>> GetAllLoggedUsersAsync()
        {
            return await _commonDependencies.UserManager.GetAllLoggedUsersAsync();
        }

        public async  Task<List<MessageModel>> GetAllMessagesAsync()
        {
            return await _messageLogic.GetAllMessagesAsync();
        }
    }
}