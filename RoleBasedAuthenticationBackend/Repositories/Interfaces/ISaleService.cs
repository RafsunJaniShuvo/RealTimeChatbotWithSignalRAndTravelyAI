using RoleBasedAuthenticationBackend.DTOs;
using RoleBasedAuthenticationBackend.Models;

namespace RoleBasedAuthenticationBackend.Repositories.Interfaces
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAllAsync();
        Task<Sale?> GetByIdAsync(int id);
        Task CreateAsync(CreateSaleDto dto);
    }
}
