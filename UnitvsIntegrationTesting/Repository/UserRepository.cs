// IUserRepository arayüzünü miras alan ve içindeki GetById metodunun gövdesini dolduran sınıftır. 

using UnitvsIntegrationTesting.Entities;

namespace UnitvsIntegrationTesting.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Veritabanı gibi davranacak sahte bir liste oluşturuyoruz.
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Deniz" },
            new User { Id = 2, Name = "Selin" }
        };

        // Sözleşmedeki GetById metodunu burada dolduruyoruz.
        public User? GetById(int id)
        {
            // Liste içinde gönderilen ID'ye sahip ilk kullanıcıyı bul.
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}