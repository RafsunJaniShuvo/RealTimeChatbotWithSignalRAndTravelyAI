using RoleBasedAuthenticationBackend.DTOs;
using RoleBasedAuthenticationBackend.Models;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;
using System.Drawing.Printing;

namespace RoleBasedAuthenticationBackend.Repositories.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;

        public async Task<IList<Product>> GetAllAsync()
        {
            IList<Product> products = await _repo.GetAllAsync();
            return products;
        }

        public async Task<(IEnumerable<ProductDto>, int)> GetAllAsync(string? search, int page, int pageSize)
        {
            var products = await _repo.GetAllAsync(search, page, pageSize);
            var totalCount = await _repo.CountAsync(search);
            var dtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                SKU = p.SKU,
                Price = p.Price,
                StockQty = p.StockQty,
                Description = p.Description
            });
            return (dtos, totalCount);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Price = product.Price,
                StockQty = product.StockQty,
                Description = product.Description
            };
        }

        public async Task CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU,
                Price = dto.Price,
                StockQty = dto.StockQty,
                Description = dto.Description
            };
            await _repo.AddAsync(product);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(int id, CreateProductDto dto)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            product.Name = dto.Name;
            product.SKU = dto.SKU;
            product.Price = dto.Price;
            product.StockQty = dto.StockQty;
            product.Description = dto.Description;

            _repo.Update(product);
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            _repo.SoftDelete(product);
            await _repo.SaveAsync();
        }

        
    }

}
