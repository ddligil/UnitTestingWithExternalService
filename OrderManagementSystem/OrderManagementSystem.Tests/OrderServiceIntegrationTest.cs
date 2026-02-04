using NUnit.Framework;
using OrderManagementSystem.Entities;
using OrderManagementSystem.Repositories;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Tests
{
    [TestFixture]
    public class OrderServiceIntegrationTests
    {
        private OrderService _orderService;
        private ProductRepository _productRepository;
        private FakePaymentService _paymentService;

        [SetUp]
        public void Setup()
        {
            _productRepository = new ProductRepository();
            _paymentService = new FakePaymentService();

            // Gerçek parçaları birleştiriyoruz
            _orderService = new OrderService(_productRepository, _paymentService);
        }

        [Test]
        public void PlaceOrder_GercekAkis_StoktanDusmeliVeBasariliOlmali()
        {
            // 1. ARRANGE
            // ProductRepository içinde varsayılan olarak ID:1 ve Stock:5 olan bir ürün var demiştik.
            int productId = 1;
            int orderQuantity = 2;
            decimal totalAmount = 50000;

            // 2. ACT
            var result = _orderService.PlaceOrder(productId, orderQuantity, totalAmount);

            // 3. ASSERT
            Assert.That(result, Is.True); // Ödeme servisi true dönecek şekilde ayarlı

            // Depodaki gerçek ürünü tekrar çekip kontrol ediyoruz
            var updatedProduct = _productRepository.GetProductById(productId);
            Assert.That(updatedProduct.Stock, Is.EqualTo(3)); // 5 - 2 = 3 kalmalı
        }
    }
}