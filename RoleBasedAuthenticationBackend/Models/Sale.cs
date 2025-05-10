using System.ComponentModel.DataAnnotations.Schema;

namespace RoleBasedAuthenticationBackend.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int QuantitySold { get; set; }
        public int CurrentStock { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; }
    }
}
