using System.Collections.Generic;

namespace CreditCardService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public List<CreditCard> CreditCards { get; set; } 
    }
}