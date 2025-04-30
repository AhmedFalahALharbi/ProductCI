using ProductApi.Models;
using ProductApi.Configuration;
using Microsoft.Extensions.Options;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;
        private readonly AppSettings _appSettings;
        private int _nextId = 1;

        public ProductService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            
            // Initialize with some sample products
            _products = new List<Product>
            {
                new Product 
                { 
                    Id = _nextId++, 
                    Name = "Laptop", 
                    Description = "High-performance laptop", 
                    Price = 1299.99m, 
                    Currency = _appSettings.DefaultCurrency 
                },
                new Product 
                { 
                    Id = _nextId++, 
                    Name = "Smartphone", 
                    Description = "Latest smartphone model", 
                    Price = 799.99m, 
                    Currency = _appSettings.DefaultCurrency 
                },
                new Product 
                { 
                    Id = _nextId++, 
                    Name = "Headphones", 
                    Description = "Noise-canceling headphones", 
                    Price = 199.99m, 
                    Currency = _appSettings.DefaultCurrency 
                }
            };
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product Add(Product product)
        {
            product.Id = _nextId++;
            
            // Set default currency if not provided
            if (string.IsNullOrEmpty(product.Currency))
            {
                product.Currency = _appSettings.DefaultCurrency;
            }
            
            _products.Add(product);
            return product;
        }
    }
}