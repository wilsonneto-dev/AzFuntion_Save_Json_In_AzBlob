using System.Net.Http;
using System.Threading.Tasks;
using AzFunc_JsonCache.Crosscutting.Interfaces;

namespace AzFunc_JsonCache.Crosscutting 
{
    public class ApiClient : IApiClient
    {
        public HttpClient httpClient { get; set; }

        public ApiClient() 
        {
            this.httpClient = new HttpClient();
        }

        public async Task<string> Get(string uri)
        {
            return await this.httpClient.GetStringAsync(uri);
        }
    }
}
