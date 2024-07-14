using Microsoft.EntityFrameworkCore;
using ProductAPI.Domain.Contract.Repositories;
using ProductAPI.Domain.Contract.Services;
using ProductAPI.Domain.Requests;
using ProductAPI.Domain.Entities;

namespace ProductAPI.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAll().ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task AddProductAsync(ProductDTO product)
        {
            await _productRepository.AddAsync(new Product
            {
                Name = product.Name,
                CostPrice = product.CostPrice,
                CreatedAt = DateTime.UtcNow,
                SellPrice = product.SellPrice
            });
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductDTO product)
        {
            _productRepository.Update(new Product
            {
                Name = product.Name,
                CostPrice = product.CostPrice,
                CreatedAt = DateTime.UtcNow,
                ProductId = product.ProductId,
                SellPrice = product.SellPrice
            });
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(long id)
        {
            await _productRepository.RemoveAsync(id);
            await _productRepository.SaveChangesAsync();
        }
    }
}