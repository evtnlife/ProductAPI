using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using ProductAPI;
using ProductAPI.Controllers;
using ProductAPI.Domain.Contract.Services;
using ProductAPI.Domain.Entities;
using ProductAPI.Domain.Requests;
using ProductAPI.Infra;
using System.Net.Http.Json;

namespace ProductApi.Tests
{
    public class ProductControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        private Mock<IProductService> _productServiceMock;
        private Mock<ILogger<ProductController>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductController>>();
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddScoped(_ => _productServiceMock.Object);
                        services.AddScoped(_ => _loggerMock.Object);

                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<ProductAPIContext>));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        services.AddDbContext<ProductAPIContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });
                    });
                });

            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetProducts_ShouldReturnProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product 1", CostPrice = 10.0M, SellPrice = 15.0M },
                new Product { ProductId = 2, Name = "Product 2", CostPrice = 20.0M, SellPrice = 25.0M }
            };

            _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var response = await _client.GetAsync("/api/product");

            // Assert
            response.EnsureSuccessStatusCode();
            var returnedProducts = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
            Assert.NotNull(returnedProducts);
            Assert.AreEqual(2, ((List<Product>)returnedProducts).Count);
        }

        [Test]
        public async Task GetProduct_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product 1", CostPrice = 10.0M, SellPrice = 15.0M };
            _productServiceMock.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var response = await _client.GetAsync("/api/product/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var returnedProduct = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(returnedProduct);
            Assert.AreEqual(product.ProductId, returnedProduct.ProductId);
        }

        [Test]
        public async Task CreateProduct_ShouldCreateProduct()
        {
            // Arrange
            var product = new ProductDTO { ProductId = 1, Name = "Product 1", CostPrice = 10.0M, SellPrice = 15.0M };
            _productServiceMock.Setup(s => s.AddProductAsync(product)).Returns(Task.CompletedTask);

            // Act
            var response = await _client.PostAsJsonAsync("/api/product", product);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadFromJsonAsync<ProductDTO>();
            Assert.NotNull(createdProduct);
            Assert.AreEqual(product.Name, createdProduct.Name);
        }

        [Test]
        public async Task UpdateProduct_ShouldUpdateProduct()
        {
            // Arrange
            var product = new ProductDTO { ProductId = 1, Name = "Updated Product", CostPrice = 10.0M, SellPrice = 15.0M };
            _productServiceMock.Setup(s => s.UpdateProductAsync(product)).Returns(Task.CompletedTask);

            // Act
            var response = await _client.PutAsJsonAsync("/api/product/1", product);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task DeleteProduct_ShouldRemoveProduct()
        {
            // Arrange
            _productServiceMock.Setup(s => s.DeleteProductAsync(1)).Returns(Task.CompletedTask);

            // Act
            var response = await _client.DeleteAsync("/api/product/1");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
