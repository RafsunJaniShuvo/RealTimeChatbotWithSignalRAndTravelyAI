using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string Sender { get; set; } = "User";
        public string Message { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
