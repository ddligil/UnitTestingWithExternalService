using System;
using OrderManagementSystem.Entities;
using OrderManagementSystem.Repositories;

namespace OrderManagementSystem.Services
{
    public class OrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPaymentService _paymentService;

        public OrderService(IProductRepository productRepository, IPaymentService paymentService)
        {
            _productRepository = productRepository;
            _paymentService = paymentService;
        }

        public bool PlaceOrder(int productId, int quantity, decimal amount)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (product.Stock < quantity)
            {
                throw new Exception("Yetersiz stok!");
            }

            bool isPaymentSuccessful = _paymentService.ProcessPayment(amount);
            if (!isPaymentSuccessful)
            {
                return false;
            }

            product.Stock -= quantity;
            _productRepository.UpdateProduct(product);

            return true;
        }
    }
}