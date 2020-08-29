using HermesModels.Interfaces;

namespace HermesModels.User
{
    public class ChannelUser : IUser // NOT USED CURRENTLY, TODO? 
    {
        public int ChannelId { get; set; }

        public Channel Channel { get; set; }

        public int Id { get; set; }

        public AppUser AppUser { get; set; }
    }
}
