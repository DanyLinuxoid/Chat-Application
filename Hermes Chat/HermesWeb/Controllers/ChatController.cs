using HermesLogic.Interfaces;
using HermesModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HermesChat.Controllers
{
    [Authorize]
    public class ChatController : ApplicationController
    {
        private readonly IChatLogic _chatLogic;

        public ChatController(IChatLogic chatLogic)
        {
            _chatLogic = chatLogic;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _chatLogic.GetChatPreloadInformationAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessageAsync(MessageModel message)
        {
            message.UserName = CurrentUser;
            var result = await _chatLogic.SendMessageAsync(message);
            return Json(result);
        }
    }
}