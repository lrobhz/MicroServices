namespace VirtualLibraryApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public CreditCard CreditCard { get; set; } 
    }
}