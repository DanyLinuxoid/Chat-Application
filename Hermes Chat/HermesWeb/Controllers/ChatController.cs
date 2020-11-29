using HermesLogic.Features.Chat.Interfaces;
using HermesModels.Chat;
using HermesWeb.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HermesWeb.Controllers
{
    /// <summary>
    /// Global chat.
    /// </summary>
    [Authorize]
    public partial class ChatController : HermesApplicationController
    {
        /// <summary>
        /// Chat logic.
        /// </summary>
        private readonly IChatLogic _chatLogic;

        /// <summary>
        /// Global chat.
        /// </summary>
        public ChatController(IChatLogic chatLogic)
        {
            _chatLogic = chatLogic;
        }

        /// <summary>
        /// Global chat page,
        /// </summary>
        [HttpGet]
        public async virtual Task<IActionResult> Index()
        {
            // As picture in navigation bar leads to chat - to avoid error page, return to login page in case if user is not logged in.
            return User.Identity.IsAuthenticated
                ? View(MVC.Chat.Views.Chat, await _chatLogic.GetChatPreloadInformationAsync())
                : View(MVC.Home.Index());
        }

        /// <summary>
        /// Sends message to global chat.
        /// </summary>
        /// <param name="message">Message to send.</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxModelStateFilter]
        public async virtual Task<IActionResult> SendMessageAsync(MessageModel message)
        {
            message.UserId = CurrentUserId;
            message.CreationTime = DateTime.Now;
            await _chatLogic.SendMessageAsync(message);
            return HermesSimpleOkResult();
        }
    }
}