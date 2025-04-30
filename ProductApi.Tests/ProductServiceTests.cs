using Xunit;
using ProductApi.Services;
using ProductApi.Models;
using ProductApi.Configuration;
using Microsoft.Extensions.Options;
using Moq;

namespace ProductApi.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly AppSettings _appSettings;

        public ProductServiceTests()
        {
            // Setup the app settings for testing
            _appSettings = new AppSettings
            {
                AppName = "Test Product Catalog",
                DefaultCurrency = "USD"
            };

            var mockOptions = new Mock<IOptions<AppSettings>>();
            mockOptions.Setup(m => m.Value).Returns(_appSettings);

            _productService = new ProductService(mockOptions.Object);
        }

        [Fact]
        public void GetAll_ReturnsAllProducts()
        {
            // Act
            var products = _productService.GetAll();

            // Assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(3, products.Count); // Assuming we have 3 default products
        }

        [Fact]
        public void GetById_WithValidId_ReturnsProduct()
        {
            // Arrange
            int testId = 1;

            // Act
            var product = _productService.GetById(testId);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(testId, product.Id);
        }

        [Fact]
        public void GetById_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int invalidId = 999;

            // Act
            var product = _productService.GetById(invalidId);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public void Add_ShouldAddProductAndAssignId()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 9.99m
            };

            // Act
            var result = _productService.Add(newProduct);
            var allProducts = _productService.GetAll();

            // Assert
            Assert.NotEqual(0, result.Id);
            Assert.Equal(_appSettings.DefaultCurrency, result.Currency);
            Assert.Contains(result, allProducts);
        }
    }
}