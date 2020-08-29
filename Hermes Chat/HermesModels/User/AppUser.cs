using HermesModels.Chat;
using System.Collections.Generic;

namespace HermesModels.User
{
    public class AppUser 
    {
        public AppUser()
        {
            Messages = new HashSet<MessageModel>();
        }

        public virtual ICollection<MessageModel> Messages { get; set; }

        public List<ChannelUser> UserChannels { get; set; }
    }
}
