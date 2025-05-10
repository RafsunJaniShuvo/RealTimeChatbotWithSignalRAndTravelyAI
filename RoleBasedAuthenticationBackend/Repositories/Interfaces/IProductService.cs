using RoleBasedAuthenticationBackend.DTOs;
using RoleBasedAuthenticationBackend.Models;

namespace RoleBasedAuthenticationBackend.Repositories.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto>, int)> GetAllAsync(string? search, int page, int pageSize);
        Task<IList<Product>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto dto);
        Task UpdateAsync(int id, CreateProductDto dto);
        Task DeleteAsync(int id);
    }
}
