using Newtonsoft.Json;
using RestSharp;
using VirtualLibrayApi.Models;

namespace VirtualLibrayApi.Auth
{
    public class AuthService
    {
        private string AuthApiBasePath = "http://127.0.0.1:50000/api/v1/Autenticacao";
        public User CheckToken(string token)
        {
            var client = new RestClient(AuthApiBasePath);
            var request = new RestRequest("check", Method.POST);
            request.AddParameter("token", token, ParameterType.RequestBody);
            //request.AddParameter("token", token, ParameterType.UrlSegment);

            var response = client.Execute(request);
            var content = response.Content;
            if(!string.IsNullOrEmpty(content))
            {
                return JsonConvert.DeserializeObject<User>(content);
            }
            else
                return null;
        }
    }
}