using RoleBasedAuthenticationBackend.DTOs;
using RoleBasedAuthenticationBackend.Models;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;

namespace RoleBasedAuthenticationBackend.Repositories.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepo;
        private readonly IProductRepository _productRepo;

        public SaleService(ISaleRepository saleRepo, IProductRepository productRepo)
        {
            _saleRepo = saleRepo;
            _productRepo = productRepo;
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _saleRepo.GetAllAsync();
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _saleRepo.GetByIdAsync(id);
        }

        public async Task CreateAsync(CreateSaleDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.ProductId)
                ?? throw new Exception("Product not found");

            if (dto.QuantitySold <= 0)
                throw new Exception("Quantity must be greater than 0");

            if (dto.QuantitySold > product.StockQty)
                throw new Exception("Insufficient stock");

            var totalPrice = product.Price * dto.QuantitySold;

            var sale = new Sale
            {
                ProductId = dto.ProductId,
                QuantitySold = dto.QuantitySold,
                CurrentStock = product.StockQty - dto.QuantitySold,
                TotalPrice = totalPrice,
                SaleDate = DateTime.UtcNow
            };

            await _saleRepo.AddAsync(sale);

            // Update product stock and save
            product.StockQty -= dto.QuantitySold;
            _productRepo.Update(product);
            await _productRepo.SaveAsync();
        }

    }
}
