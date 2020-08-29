using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HermesLogic.Interfaces;
using HermesModels.Chat;

namespace HermesChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUserManager _userManager;
        private readonly IChatLogic _chatLogic;

        public ChatHub(IChatLogic chatLogic,
                               IUserManager userManager)
        {
            _chatLogic = chatLogic;
            _userManager = userManager;
        }

        public async Task SendMessage(MessageModel message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
            _chatLogic.SendMessageAsync(message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("newUserJoined", Context.User.Identity.Name);
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            //var user = _userManager.GetUser(new string[] { Context.User.Identity.Name }, HermesModels.Enums.UserRetrieveOption.GetByDomainName); // change retrieve logic
            await Clients.All.SendAsync("newUserJoined", Context.User.Identity.Name); // change to other method
            //_userManager.UpdateUserInformation(user, new { IsLogged = false }); /// SHOULD NOT BE LIKE THAT !!!!!!!!!!!!!  MUST GO THROUGH AUTH SERVICE
        }
    }
}