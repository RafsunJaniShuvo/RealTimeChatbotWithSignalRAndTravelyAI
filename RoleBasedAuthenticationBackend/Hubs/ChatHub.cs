using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITavilyService _tavilyService;
        public ChatHub(UserManager<ApplicationUser> userManager, ITavilyService tavilyService)
        {
            _userManager = userManager;
            _tavilyService = tavilyService;
        }
        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if (user != null)
            {
                Console.WriteLine($"User connected: {user.UserName}");
            }
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(string userMessage)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var userName = user?.UserName ?? "Anonymous";

            var botResponse = await _tavilyService.GetBotResponseAsync(userMessage);
            await Clients.All.SendAsync("ReceiveMessage", userName, botResponse);
        }
    }
}
