using System.Net.Http;

namespace Sampaio.Shared.Utils
{
    public class BaseHttpClient
    {
        public readonly HttpClient Client;

        public BaseHttpClient()
        {
            Client = new HttpClient();
        }
    }
}