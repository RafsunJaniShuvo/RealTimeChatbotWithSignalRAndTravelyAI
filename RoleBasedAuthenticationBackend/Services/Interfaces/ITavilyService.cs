namespace WebApi.Services.Interfaces
{
    public interface ITavilyService
    {
        Task<string> GetBotResponseAsync(string userMessage);
    }
}
