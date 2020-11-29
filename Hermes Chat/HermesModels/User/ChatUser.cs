using HermesModels.Base;

namespace HermesModels.User
{
    public class ChatUser
    {
        public long AspNetUserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public long? AccountImageId { get; set; }

        public FileBase AccountImage { get; set; }
    }
}
