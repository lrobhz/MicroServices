namespace VirtualLibraryApi.Models
{
    public class PaymentDTO
    {
        public int UserId { get; set; }
        public float Value { get; set; }
        public string CreditCardToken { get; set; }
    }
}