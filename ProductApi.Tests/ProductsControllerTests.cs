// ProductApi.Tests/ProductsControllerTests.cs
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Controllers;
using ProductApi.Services;
using ProductApi.Models;
using ProductApi.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace ProductApi.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductsController _controller;
        private readonly List<Product> _testProducts;

        public ProductsControllerTests()
        {
            // Create mock service
            _mockService = new Mock<IProductService>();
            
            // Setup app settings
            var appSettings = new AppSettings 
            { 
                AppName = "Test Product API", 
                DefaultCurrency = "USD" 
            };
            var mockOptions = new Mock<IOptions<AppSettings>>();
            mockOptions.Setup(m => m.Value).Returns(appSettings);

            // Create controller with mock service
            _controller = new ProductsController(_mockService.Object, mockOptions.Object);

            // Setup test data
            _testProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1", Description = "Description 1", Price = 10.99m, Currency = "USD" },
                new Product { Id = 2, Name = "Test Product 2", Description = "Description 2", Price = 20.99m, Currency = "USD" }
            };
        }

        [Fact]
        public void GetProducts_ReturnsAllProducts()
        {
            // Arrange
            _mockService.Setup(service => service.GetAll())
                .Returns(_testProducts);

            // Act
            var result = _controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            var returnValue = Assert.IsType<List<Product>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetProduct_WithValidId_ReturnsProduct()
        {
            // Arrange
            var testProduct = _testProducts[0];
            _mockService.Setup(service => service.GetById(testProduct.Id))
                .Returns(testProduct);

            // Act
            var result = _controller.GetProduct(testProduct.Id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            var returnValue = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal(testProduct.Id, returnValue.Id);
            Assert.Equal(testProduct.Name, returnValue.Name);
        }

        [Fact]
        public void GetProduct_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 999;
            _mockService.Setup(service => service.GetById(invalidId))
                .Returns((Product)null);

            // Act
            var result = _controller.GetProduct(invalidId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void CreateProduct_AddsProductAndReturnsCreatedResponse()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "New Product",
                Description = "New Description",
                Price = 15.99m,
                Currency = "USD"
            };

            var createdProduct = new Product
            {
                Id = 3,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                Currency = newProduct.Currency
            };

            _mockService.Setup(service => service.Add(It.IsAny<Product>()))
                .Returns(createdProduct);

            // Act
            var result = _controller.CreateProduct(newProduct);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            
            Assert.Equal("GetProduct", createdAtActionResult.ActionName);
            Assert.Equal(createdProduct.Id, createdAtActionResult.RouteValues["id"]);
            
            var returnValue = Assert.IsType<Product>(createdAtActionResult.Value);
            Assert.Equal(createdProduct.Id, returnValue.Id);
            Assert.Equal(newProduct.Name, returnValue.Name);
        }
    }
}