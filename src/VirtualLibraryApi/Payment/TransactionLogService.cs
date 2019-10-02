using RestSharp;
using VirtualLibraryApi.Models;

namespace VirtualLibraryApi.Payment
{
    public class TransactionLogService
    {
        private string transactionBasePath = "https://localhost:7001/api/v1/public/Transacoes";
        public IRestResponse Log(Transacao transacao)
        {
            var client = new RestClient(transactionBasePath);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(transacao);

            var response = client.Execute(request);
            
            return response;
        }
    }
}