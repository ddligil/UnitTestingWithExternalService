namespace OrderManagementSystem.Services
{
    // IPaymentService sözleşmesine uyan bir sınıf oluşturuyoruz
    public class FakePaymentService : IPaymentService
    {
        public bool ProcessPayment(decimal amount)
        {
            // Gerçek dünyada burada banka API çağrıları olurdu.
            // Şimdilik test amaçlı her ödemeyi onaylıyoruz.
            return amount > 0; 
        }
    }
}