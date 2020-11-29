using HermesModels.Base;
using HermesModels.Chat;

namespace HermesModels.MVC
{
    /// <summary>
    /// Model that represents chat user, message sender.
    /// </summary>
    public class MessageSenderModel
    {
        /// <summary>
        /// User profile image.
        /// </summary>
        public FileBase UserAccountImage { get; set; }

        /// <summary>
        /// Message model.
        /// </summary>
        public MessageModel MessageModel { get; set; }
    }
}
