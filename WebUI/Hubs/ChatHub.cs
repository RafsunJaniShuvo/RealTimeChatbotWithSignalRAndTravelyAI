using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebUI.Models;
//using WebApi.Models;
//using WebApi.Services.Interfaces;

namespace WebUI.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage chatMessage)
        {
            await Clients.Caller.SendAsync("BotIsTyping", true);

            await Clients.All.SendAsync("ReceiveMessage", chatMessage.Sender, chatMessage.Message);
        }
    }
}
