using Microsoft.EntityFrameworkCore;
using RoleBasedAuthenticationBackend.Data;
using RoleBasedAuthenticationBackend.Models;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;
using System.Data.Common;

namespace RoleBasedAuthenticationBackend.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) => _context = context;

        public async Task<IList<Product>> GetAllAsync()
        {
            try
            {
                return await _context.Products
                    .Where(p => !p.IsDeleted)
                    .ToListAsync();
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to retrieve product list. Please try again later.", ex);
            }
        }

        public async Task<IList<Product>> GetAllAsync(string? search, int page, int pageSize)
        {
            try
            {
                var query = _context.Products
                    .Where(p => !p.IsDeleted &&
                        (string.IsNullOrEmpty(search) ||
                         p.Name.Contains(search) ||
                         p.SKU.Contains(search))).OrderByDescending(p => p.CreatedAt);

                return await query
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to retrieve paginated products. Invalid page or search criteria.", ex);
            }
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException( $"Failed to retrieve product with ID {id}. The product may not exist.",ex);
            }
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to add new product. The product data may be invalid.", ex);
            }
        }

        public void Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to update product. The product may have been modified or deleted by another user.",ex);
            }
        }

        public void SoftDelete(Product product)
        {
            try
            {
                product.IsDeleted = true;
                _context.Products.Update(product);
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to delete product. The product may no longer exist.",ex);
            }
        }

        public async Task<int> CountAsync(string? search)
        {
            try
            {
                return await _context.Products
                    .Where(p => !p.IsDeleted &&
                          (string.IsNullOrEmpty(search) ||
                           p.Name.Contains(search) ||
                           p.SKU.Contains(search)))
                    .CountAsync();
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to count products. The search criteria may be invalid.",ex);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to save changes. Please verify your data and try again.",ex);
            }
        }
    }

}
