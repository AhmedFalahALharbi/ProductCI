// ProductApi.Tests/ProductApiIntegrationTests.cs
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;
using ProductApi.Models;

namespace ProductApi.Tests
{
    public class ProductApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ProductApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ReturnsSuccessAndProducts()
        {
            // Act
            var response = await _client.GetAsync("/api/products");
            
            // Assert
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            
            Assert.NotNull(products);
            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetProductById_WithValidId_ReturnsProduct()
        {
            // Arrange
            int productId = 1;
            
            // Act
            var response = await _client.GetAsync($"/api/products/{productId}");
            
            // Assert
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<Product>();
            
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
        }

        [Fact]
        public async Task GetProductById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 999;
            
            // Act
            var response = await _client.GetAsync($"/api/products/{invalidId}");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateProduct_AddsNewProduct()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "Integration Test Product",
                Description = "Created during integration test",
                Price = 99.99m,
                Currency = "USD"
            };
            
            var content = new StringContent(
                JsonSerializer.Serialize(newProduct),
                Encoding.UTF8,
                "application/json");
            
            // Act
            var response = await _client.PostAsync("/api/products", content);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
            
            Assert.NotNull(createdProduct);
            Assert.NotEqual(0, createdProduct.Id);
            Assert.Equal(newProduct.Name, createdProduct.Name);
            Assert.Equal(newProduct.Description, createdProduct.Description);
            Assert.Equal(newProduct.Price, createdProduct.Price);
        }
    }
}