// (Interface): Bu bir bağımlılıktır. Yani bizim uygulamamızın dışında kalan, internet üzerinden erişilen veya başka bir ekibin yazdığı bir "hizmettir". 
// Test yaparken bu hizmeti kolayca "taklit etmek" (Mocking) zorunda olduğumuz için interface yaptık.
namespace UnitvsIntegrationTesting.Services
{
    public interface IProfileImageService
    {
        // "Bana kullanıcı numarasını ver, ben de sana internetteki resim linkini döneyim"
        string GetImageUrl(int userId);
    }
}