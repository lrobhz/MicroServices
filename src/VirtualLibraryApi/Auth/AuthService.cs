using Newtonsoft.Json;
using RestSharp;
using VirtualLibraryApi.Models;

namespace VirtualLibraryApi.Auth
{
    public class AuthService
    {
        private string AuthApiBasePath = "http://localhost:50000/api/v1/Autenticacao";
        public User CheckToken(string token)
        {
            var client = new RestClient(AuthApiBasePath);
            var request = new RestRequest($"check/{token}", Method.POST);

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