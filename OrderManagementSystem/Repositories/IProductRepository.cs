using OrderManagementSystem.Entities;

namespace OrderManagementSystem.Repositories
{
    public interface IProductRepository
    {
        Product GetProductById(int id);
        void UpdateProduct(Product product);
    }
}