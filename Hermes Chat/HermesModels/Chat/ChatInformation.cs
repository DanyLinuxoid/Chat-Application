using HermesModels.MVC;
using HermesModels.User;

using System.Collections.Generic;

namespace HermesModels.Chat
{
    public class ChatInformation : MvcBaseModel
    {
        public List<AspNetUser> UsersOnline { get; set; }

        public List<MessageModel> Messages { get; set; }

        public MessageModel UserMessage { get; set; }
    }
}
