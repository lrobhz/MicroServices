using RestSharp;
using VirtualLibraryApi.Models;

namespace VirtualLibraryApi.Payment
{
    public class CreditCardService
    {
        private string creditCardApiBasePath = "http://localhost:6000/api/v1/CreditCard";
        public bool Pay(string token)
        {
            var client = new RestClient(creditCardApiBasePath);
            var request = new RestRequest($"pay/{token}", Method.POST);

            var response = client.Execute(request);
            
            return response.IsSuccessful;
        }
    }
}