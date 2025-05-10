using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.DTOs;
using WebApi.Hubs;
using WebApi.Models;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        //private readonly IChatMessageRepository _chatRepo;
        private readonly ITavilyService _tavilyService;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatController(IChatMessageRepository chatMessageRepository, ITavilyService tavilyService, IHubContext<ChatHub> hubContext, UserManager<ApplicationUser> userManager)
        {
            _chatMessageRepository = chatMessageRepository;
            _tavilyService = tavilyService;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] string userMessage)
        {

            var sessionId = Guid.NewGuid().ToString(); 
            var userId = "user123"; 
            var timestamp = DateTime.UtcNow;

            // 1. Save user's message
            var userChatMessage = new ChatMessage
            {
                UserId = Guid.NewGuid().ToString(),
                SessionId = sessionId,
                Sender = "User",
                Message = userMessage,
                Timestamp = DateTime.UtcNow
            };

            var botReply = await _tavilyService.GetBotResponseAsync(userMessage);

            // 3. Save bot's response
            var botChatMessage = new ChatMessage
            {
                UserId = userId,
                SessionId = sessionId,
                Sender = "Bot",
                Message = botReply,
                Timestamp = DateTime.UtcNow
            };

            var savedBotMessage = await _chatMessageRepository.AddMessageAsync(botChatMessage);

            // 4. Return both messages
            return Ok(new
            {
                UserMessage = "",//savedUserMessage,
                BotMessage = savedBotMessage
            });
        }
        

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] string sessionId)
        {
            var messages = await _chatMessageRepository.GetMessagesAsync(sessionId);
            return Ok(messages);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMessage(int id, [FromBody] string newMessage)
        {
            
            var message = await _chatMessageRepository.UpdateMessageAsync(new ChatMessage { Id = id, Message = newMessage });
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _chatMessageRepository.DeleteMessageAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> ApproveMessage(int id)
        {
            await _chatMessageRepository.ApproveMessageAsync(id);
            return NoContent();
        }
    }
}
