using Moq;
using NUnit.Framework; 
using UnitvsIntegrationTesting.Entities;
using UnitvsIntegrationTesting.Repositories;
using UnitvsIntegrationTesting.Services;

namespace UnitvsIntegrationTesting.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockRepo; // Moq kullanarak IUserRepository için bir mock nesnesi oluşturuyoruz.

        private Mock<IProfileImageService> _mockImageService; // Moq kullanarak IProfileImageService için bir mock nesnesi oluşturuyoruz.
        private UserService _userService; // Test etmek istediğimiz servis.

        // [SetUp]: Her test metodundan önce çalışan kısımdır. 
        // Ortak hazırlıkları burada yaparsın.
        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockImageService = new Mock<IProfileImageService>();
            //Sahte repository hazır
            // Service bu sahte repository’yi kullanıyor

            _userService = new UserService(_mockRepo.Object, _mockImageService.Object); //Test edeceğimiz servise bu sahte repository'i veriyoruz.
        }

        [Test] 
        public void GetUser_KullaniciVarken_UserDonmeli()
        {
            // Arrange
            var expectedUser = new User { Id = 1, Name = "Deniz" };
            var expectedUrl = "https://cdn.com/deniz.jpg"; // Fotoğraf linkimiz


            _mockRepo.Setup(repo => repo.GetById(1)).Returns(expectedUser); //Setup():Moq metodu “Şu çağrı gelirse, şu cevabı ver” der
            _mockImageService.Setup(service => service.GetImageUrl(1)).Returns(expectedUrl);

            // Act
            var result = _userService.GetUser(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Deniz"));
            Assert.That(result.ProfilePictureUrl, Is.EqualTo(expectedUrl));
        }

        [Test]
        public void GetUser_KullaniciYokken_HataFirlatmali()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((User?)null); //Herngi id olursa olsun getbyid çağrıldığında bana null dönecek şekilde ayarlıyoruz.

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _userService.GetUser(99));
            Assert.That(ex.Message, Is.EqualTo("Kullanıcı bulunamadı!")); //Mesajı da kontrol ediyorum

        }
        [Test]
        public void GetUser_DisServisHataVerirse_HataYukariFirlatilmali()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetById(1)).Returns(new User { Id = 1, Name = "Deniz" });
            
            // Fotoğrafçı hata verirse ne olur?
            _mockImageService.Setup(s => s.GetImageUrl(1)).Throws(new Exception("Bağlantı koptu!"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _userService.GetUser(1));
            Assert.That(ex.Message, Is.EqualTo("Bağlantı koptu!"));
        }
    }
}