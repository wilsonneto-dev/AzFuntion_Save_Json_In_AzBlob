using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

using AzFunc_JsonCache.Crosscutting.Interfaces;

namespace AzFunc_JsonCache.Crosscutting
{
    public class AzureStorageService : IStorageService {
        private String ConnectionString;
    
        public AzureStorageService(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public async Task<String> SendTextContent(String content, string storageFile, string container = "general")
        {
            
            CloudStorageAccount azureAccount = CloudStorageAccount.Parse(this.ConnectionString);
            CloudBlobClient cloudBlobClient = azureAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(container);

            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(storageFile);
            blockBlob.Properties.ContentType = "text/json";
            
            await blockBlob.UploadTextAsync(content);
            return blockBlob.Uri.ToString();
        }
    }
}