using WebApi.Models;

namespace WebApi.Repositories.Interfaces
{
    public interface IChatMessageRepository
    {
        Task<ChatMessage> AddMessageAsync(ChatMessage message);
        Task<IEnumerable<ChatMessage>> GetMessagesAsync(string sessionId);
        Task<ChatMessage> UpdateMessageAsync(ChatMessage message);
        Task DeleteMessageAsync(int messageId);
        Task ApproveMessageAsync(int messageId);
    }
}
