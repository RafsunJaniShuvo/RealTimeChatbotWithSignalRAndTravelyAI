using RoleBasedAuthenticationBackend.Models;

namespace RoleBasedAuthenticationBackend.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAllAsync(string? search, int page, int pageSize);
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void SoftDelete(Product product);
        Task<int> CountAsync(string? search);
        Task SaveAsync();
        Task<IList<Product>> GetAllAsync();
    }

}
