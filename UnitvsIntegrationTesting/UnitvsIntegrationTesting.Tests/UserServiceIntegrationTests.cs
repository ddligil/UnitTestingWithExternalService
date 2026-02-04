//Integration Test: Service + Gerçek Repo = Uyum Testi.

using Moq;
using NUnit.Framework;
using UnitvsIntegrationTesting.Entities;
using UnitvsIntegrationTesting.Repositories;
using UnitvsIntegrationTesting.Services;


namespace UnitvsIntegrationTesting.Tests
{
    [TestFixture]
    public class UserServiceIntegrationTests
    {
        private UserService _userService; //gerçek servis
        private IUserRepository _userRepository; //gerçek repository
        private IProfileImageService _imageService; // gerçek/ fake image service


        [SetUp]
        public void Setup()
        {
            _userRepository = new UserRepository(); //Gerçek repository'yi kullanıyoruz
            _imageService = new ProfileImageService();
            _userService = new UserService(_userRepository, _imageService); //Gerçek repository'yi gerçek servise veriyoruz
        }

        [Test]
        public void GetUser_KullaniciVarken_UserDonmeli()
        {   
            
            //Act
            // UserRepository içindeki listede 1 numaralı kişi "Deniz" idi.
            var result = _userService.GetUser(1);
            Assert.That(result.Name, Is.EqualTo("Deniz"));
            // Fotoğraf servisinin de gerçek linki eklediğini kontrol edelim:
            Assert.That(result.ProfilePictureUrl, Does.Contain("api.dis-servis.com"));
        }

        [Test]
        public void GetUser_KullaniciYokken_HataFirlatmali()
        {
            //Act & Assert
            // UserRepository içindeki listede 99 numaralı kişi yok.
            var ex = Assert.Throws<Exception>(() => _userService.GetUser(99));
            Assert.That(ex.Message, Is.EqualTo("Kullanıcı bulunamadı!"));
        }

       
    }




}