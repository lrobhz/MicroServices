using Microsoft.AspNetCore.Mvc;
using RestSharp;
using VirtualLibraryApi.Models;

namespace VirtualLibraryApi.Payment
{
    public class CreditCardService
    {
        private string creditCardApiBasePath = "https://localhost:6001/api/v1/CreditCard";
        public IRestResponse Pay(PaymentDTO paymentDTO)
        {
            var client = new RestClient(creditCardApiBasePath);
            var request = new RestRequest("pay", Method.POST);
            request.AddJsonBody(paymentDTO);

            var response = client.Execute(request);
            
            return response;
        }
    }
}