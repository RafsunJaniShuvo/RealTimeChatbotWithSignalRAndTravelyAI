using Microsoft.EntityFrameworkCore;
using RoleBasedAuthenticationBackend.Data;
using System.Linq;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly AppDbContext _context;

        public ChatMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChatMessage> AddMessageAsync(ChatMessage message)
        {
            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<IEnumerable<ChatMessage>> GetAllChatAsync()
        {
            return await _context.ChatMessages.Where(x => x.Sender != "Bot" && !x.IsDeleted).OrderByDescending(x => x.Timestamp).ToListAsync();
        }
        public async Task<ChatMessage> GetMessagesAsync(string sessionId)
        {
            return await _context.ChatMessages
                                 .Where(m => m.SessionId == sessionId && !m.IsDeleted)
                                 .OrderBy(m => m.Timestamp)
                                 .FirstOrDefaultAsync();
        }


        public async Task<ChatMessage> UpdateMessageAsync(ChatMessage updatedMessage)
        {
            //_context.ChatMessages.Update(message);
            //await _context.SaveChangesAsync();
            //return message;
            try
            {
                var existingMessage = await _context.ChatMessages.FindAsync(updatedMessage.Id);
                if (existingMessage == null)
                {
                    throw new Exception("Message not found.");
                }

                // Update only the fields that have changed
                existingMessage.Message = updatedMessage.Message;
                // Add other fields as necessary

                await _context.SaveChangesAsync();
                return existingMessage;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw;
            }

        }

        public async Task DeleteMessageAsync(int messageId)
        {
            var message = await _context.ChatMessages.FindAsync(messageId);
            if (message != null)
            {
                message.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApproveMessageAsync(int messageId)
        {
            var message = await _context.ChatMessages.FindAsync(messageId);
            if (message != null)
            {
                message.IsApproved = true;
                await _context.SaveChangesAsync();
            }
        }
    }

}
