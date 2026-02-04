using UnitvsIntegrationTesting.Entities;
using UnitvsIntegrationTesting.Repositories;
using UnitvsIntegrationTesting.Services;

namespace UnitvsIntegrationTesting.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileImageService _imageService;

        public UserService(IUserRepository userRepository, IProfileImageService imageService)
        {
            _userRepository = userRepository;
            _imageService = imageService;
        } 

        public User GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı!");
            }

            user.ProfilePictureUrl = _imageService.GetImageUrl(id);

            return user;
        }
    }
}