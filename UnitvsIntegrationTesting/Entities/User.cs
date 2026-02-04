namespace UnitvsIntegrationTesting.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty; //null olmamasını sağlıyoruz.
        public string? ProfilePictureUrl { get; set; } // Dış servisten gelecek olan veri
    }
}