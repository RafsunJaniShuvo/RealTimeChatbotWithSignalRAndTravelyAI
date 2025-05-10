using System.ComponentModel.DataAnnotations;

namespace RoleBasedAuthenticationBackend.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        public string? SKU { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQty { get; set; }

        public string? Description { get; set; }
    }

    public class ProductDto : CreateProductDto
    {
        public int Id { get; set; }
    }

}
