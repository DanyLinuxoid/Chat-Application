using HermesModels.MVC;
using HermesModels.User;
using System.Collections.Generic;

namespace HermesModels.Chat
{
    /// <summary>
    /// Model to represent global chat.
    /// </summary>
    public class ChatInformationViewModel
    {
        /// <summary>
        /// Users online.
        /// </summary>
        public List<ChatUser> UsersOnline { get; set; }

        /// <summary>
        /// Users that participated in sending messages.
        /// </summary>
        public List<MessageSenderModel> ChatSendersWithMessages { get; set; }

        /// <summary>
        /// Place holder for message that user can send.
        /// </summary>
        public MessageModel UserMessageToSendPlaceHolder { get; set; }
    }
}
