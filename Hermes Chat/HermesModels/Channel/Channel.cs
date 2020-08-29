using HermesModels.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HermesModels
{
    public class Channel
    {
        public const int TYPE_OPEN_GROUP = 11;

        public const int TYPE_DIRECT = 12;

        public int ID { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        public int Type { get; set; }

        public IList<ChannelUser> ChannelUsers { get; set; }

    }
}
