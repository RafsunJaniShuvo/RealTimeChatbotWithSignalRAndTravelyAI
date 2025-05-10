using RoleBasedAuthenticationBackend.Models;

namespace RoleBasedAuthenticationBackend.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetAllAsync();
        Task<Sale?> GetByIdAsync(int id);
        Task AddAsync(Sale sale);
    }
}
