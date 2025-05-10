using Microsoft.EntityFrameworkCore;
using RoleBasedAuthenticationBackend.Data;
using RoleBasedAuthenticationBackend.Models;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;
using System.Data.Common;

namespace RoleBasedAuthenticationBackend.Repositories.Implementations
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            try
            {
                return await _context.Sales
                    .Include(s => s.Product)
                    .ToListAsync();
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to retrieve sales list. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while fetching sales.", ex);
            }
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Sales
                    .Include(s => s.Product)
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException($"Failed to retrieve sale with ID {id}. The sale may not exist.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while fetching sale with ID {id}.", ex);
            }
        }

        public async Task AddAsync(Sale sale)
        {
            try
            {
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to create new sale. The product may be invalid or unavailable.", ex);
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Database error while creating sale. Please verify your data.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating sale.", ex);
            }
        }
    }
}
