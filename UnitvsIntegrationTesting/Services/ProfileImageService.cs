namespace UnitvsIntegrationTesting.Services
{
    // Bu sınıf, gerçekten internete giden bir servisi simüle eden GERÇEK bir sınıftır.
    public class ProfileImageService : IProfileImageService
    {
        public string GetImageUrl(int userId)
        {
    
            return $"https://api.dis-servis.com/v1/photos/{userId}.jpg";
        }
    }
}