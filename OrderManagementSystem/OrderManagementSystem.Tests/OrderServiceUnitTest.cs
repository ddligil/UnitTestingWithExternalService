using Moq;
using NUnit.Framework; 
using OrderManagementSystem.Entities;
using OrderManagementSystem.Repositories;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Tests
{
    [TestFixture] 
    public class OrderServiceTests
    {
        private OrderService _orderService;
        private Mock<IProductRepository> _mockRepo;
        private Mock<IPaymentService> _mockPayment;

        [SetUp] 
        public void Setup()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockPayment = new Mock<IPaymentService>();

            _orderService = new OrderService(_mockRepo.Object, _mockPayment.Object);
        }

        [Test]
        public void PlaceOrder_HerSeyYolundaysa_TrueDonmeliVeStokDusmeli()
        {
            // 1. ARRANGE (Hazırlık)
            var fakeProduct = new Product { Id = 1, Stock = 10 };
            _mockRepo.Setup(r => r.GetProductById(1)).Returns(fakeProduct);
            _mockPayment.Setup(p => p.ProcessPayment(It.IsAny<decimal>())).Returns(true); // Banka Onayladı
            
            // 2. ACT (Eylem)
            var result = _orderService.PlaceOrder(1, 2, 100); // 1 nolu üründen 2 tane al

            // 3. ASSERT (Doğrulama)
            Assert.That(result, Is.True); // Sonuç başarılı mı?
            
            // Stok gerçekten 10'dan 8'e düştü mü?
            Assert.That(fakeProduct.Stock, Is.EqualTo(8));

            // Depo güncelleme metodu (UpdateProduct) gerçekten 1 kere çağrıldı mı?
            _mockRepo.Verify(r => r.UpdateProduct(It.IsAny<Product>()), Times.Once);
        }
    }
}