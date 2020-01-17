using System;
using System.IO;
using System.Threading.Tasks;

namespace AzFunc_JsonCache.Crosscutting.Interfaces
{
    public interface IStorageService
    {
        public Task<String> SendTextContent(String content, string storageFile, string container);
    }
}
