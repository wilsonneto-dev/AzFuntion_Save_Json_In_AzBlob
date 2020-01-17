using System.Threading.Tasks;

namespace AzFunc_JsonCache.Crosscutting.Interfaces 
{
    public interface IApiClient 
    {
        public Task<string> Get(string uri);
    }
}