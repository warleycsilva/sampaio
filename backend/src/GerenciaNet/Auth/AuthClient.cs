using System.Net.Http;
using System.Threading.Tasks;
using Sampaio.Shared.Config;
using Sampaio.Shared.Utils;
using Gerencianet.NETCore.SDK;

namespace GerenciaNet.Auth
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpClient;
        private readonly GerenciaNetConfig _config;
        
        public AuthClient(BaseHttpClient baseHttpClient, GerenciaNetConfig config)
        {
            _httpClient = baseHttpClient.Client;
            _config = config;
        }
        
        public async Task<string> CreateAccessTokenAsync(GerenciaNetConfig config)
        {
            var endpoints = new Endpoints(_config.ClientId, _config.ClientSecret, _config.Sandbox, _config.Credentials);
            return "Ok";
        }
    }
}