using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoleBasedAuthenticationBackend.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

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
