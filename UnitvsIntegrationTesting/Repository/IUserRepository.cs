using UnitvsIntegrationTesting.Entities;

namespace UnitvsIntegrationTesting.Repositories
{
    public interface IUserRepository
    {
        User? GetById(int id);
    }
}