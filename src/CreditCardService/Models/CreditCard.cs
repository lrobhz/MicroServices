using Newtonsoft.Json;

namespace CreditCardService.Models
{
    public class CreditCard
    {
        [JsonIgnore]
        public string Number { get; set; }
        [JsonIgnore]
        public string ExpirationDate { get; set; }
        [JsonIgnore]
        public string SecurityCode { get; set; }
        public string SecureNumber 
        { 
            get
            {
                return $"****.****.****.{Number.Substring(Number.Length - 4)}";
            } 
        }
        public string CreditCardToken { get; set; }
        public float Limit { get; set; } = 1000;
    }
}