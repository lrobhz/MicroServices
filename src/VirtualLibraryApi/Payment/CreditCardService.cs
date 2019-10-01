using Microsoft.AspNetCore.Mvc;
using RestSharp;
using VirtualLibraryApi.Models;

namespace VirtualLibraryApi.Payment
{
    public class CreditCardService
    {
        private string creditCardApiBasePath = "http://localhost:6000/api/v1/CreditCard";
        public IRestResponse Pay(PaymentDTO paymentDTO)
        {
            var client = new RestClient(creditCardApiBasePath);
            var request = new RestRequest($"pay", Method.POST);
            request.AddParameter("paymentDTO", paymentDTO, ParameterType.RequestBody);

            var response = client.Execute(request);
            
            return response;
        }
    }
}