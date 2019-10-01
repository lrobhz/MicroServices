using RestSharp;
using VirtualLibraryApi.Models;

namespace VirtualLibraryApi.Payment
{
    public class TransactionLogService
    {
        private string transactionBasePath = "http://localhost:6000/api/v1/public/Transacoes";
        public IRestResponse Log(Transacao transacao)
        {
            var client = new RestClient(transactionBasePath);
            var request = new RestRequest(Method.POST);
            request.AddParameter("transacao", transacao, ParameterType.RequestBody);

            var response = client.Execute(request);
            
            return response;
        }
    }
}