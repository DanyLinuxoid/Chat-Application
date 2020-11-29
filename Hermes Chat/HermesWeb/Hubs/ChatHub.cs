using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HermesModels.Chat;
using HermesLogic.Base.UserManagement;
using HermesModels.User;

namespace HermesWeb.Hubs
{
    /// <summary>
    /// ChatHub for signalR.
    /// </summary>
    [Authorize]
    public class ChatHub : Hub
    {
        /// <summary>
        /// User management.
        /// </summary>
        public readonly IUserManager _userManager;

        /// <summary>
        /// ChatHub for signalR.
        /// </summary>
        public ChatHub(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Sends message to hub, invokes function on frontend which displays passed message.
        /// </summary>
        /// <param name="message">Message to send though hub.</param>
        public async Task SendMessageAsync(MessageModel message)
        {
            ChatUser currentUser = _userManager.CurrentUser;
            await Clients.All.SendAsync("receiveMessage", 
                new
                {
                    Username = currentUser.Username,
                    Text = message.Text,
                    AccountImageData = currentUser.AccountImage.Data,
                });
        }
    }
}