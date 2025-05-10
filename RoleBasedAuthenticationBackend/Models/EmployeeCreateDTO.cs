using System.ComponentModel.DataAnnotations;

namespace RoleBasedAuthenticationBackend.Models
{
    public class EmployeeCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string? PhoneNumber { get; set; } // New property

        [Required]
        public string Position { get; set; }

        [Required]
        public string Company { get; set; }
    }
}
