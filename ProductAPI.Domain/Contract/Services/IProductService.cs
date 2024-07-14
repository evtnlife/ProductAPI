using ProductAPI.Domain.Requests;
using ProductAPI.Domain.Entities;

namespace ProductAPI.Domain.Contract.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(long id);
        Task AddProductAsync(ProductDTO product);
        Task UpdateProductAsync(ProductDTO product);
        Task DeleteProductAsync(long id);
    }

}
