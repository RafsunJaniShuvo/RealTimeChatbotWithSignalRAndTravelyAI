using WebApi.Models;

namespace WebApi.Repositories.Interfaces
{
    public interface IChatMessageRepository
    {
        Task<ChatMessage> AddMessageAsync(ChatMessage message);
        Task<ChatMessage> GetMessagesAsync(string sessionId);
        Task<IEnumerable<ChatMessage>> GetAllChatAsync();
        Task<ChatMessage> UpdateMessageAsync(ChatMessage message);
        Task DeleteMessageAsync(int messageId);
        Task ApproveMessageAsync(int messageId);
    }
}
