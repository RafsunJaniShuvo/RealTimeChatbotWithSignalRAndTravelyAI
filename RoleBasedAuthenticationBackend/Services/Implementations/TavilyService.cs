using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations
{
    public class TavilyService : ITavilyService
    {
        private readonly HttpClient _httpClient;

        public TavilyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetBotResponseAsync(string userMessage)
        {
            //var requestBody = new { query = userMessage };
            //var response = await _httpClient.PostAsJsonAsync("search", requestBody);
            //response.EnsureSuccessStatusCode();
            //var rawJson = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(rawJson); // Paste this output here if needed
            //var result = await response.Content.ReadFromJsonAsync<TavilyResponse>();
            //return result?.Answer ?? "I'm sorry, I couldn't find an answer.";
            var requestBody = new
            {
                query = userMessage,
                topic = "general",
                search_depth = "basic",
                chunks_per_source = 3,
                max_results = 1,
                time_range = (string?)null,
                days = 7,
                include_answer = true,
                include_raw_content = false,
                include_images = false,
                include_image_descriptions = false,
                include_domains = new string[] { },
                exclude_domains = new string[] { }
            };

            var response = await _httpClient.PostAsJsonAsync("search", requestBody);
            response.EnsureSuccessStatusCode();

            var rawJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine("RAW JSON:\n" + rawJson); // Optional debugging

            var result = JsonSerializer.Deserialize<TavilyResponse>(rawJson);
            return result?.Answer ?? "I'm sorry, I couldn't find an answer.";
        }
    }
    public class TavilyResponse
    {
        [JsonPropertyName("answer")]
        public string Answer { get; set; }

        [JsonPropertyName("documents")]
        public List<object> Documents { get; set; }
    }

}
