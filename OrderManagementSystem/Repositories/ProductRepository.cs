using System.Collections.Generic;
using System.Linq;
using OrderManagementSystem.Entities;

namespace OrderManagementSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 25000, Stock = 5 },
            new Product { Id = 2, Name = "Mouse", Price = 500, Stock = 10 }
        };

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }
            }
}
}