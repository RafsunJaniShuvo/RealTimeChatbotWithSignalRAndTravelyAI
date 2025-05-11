using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class ChatRequestDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; } 

        [Required(ErrorMessage = "UserMessage is required.")]
        public string UserMessage { get; set; } 
    }
}
