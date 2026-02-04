// Dış Servisi (External Service)
namespace OrderManagementSystem.Services
{
    public interface IPaymentService
    {
        bool ProcessPayment(decimal amount);
    }
}