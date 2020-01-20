using Microsoft.WindowsAzure.Storage.Table;

namespace AzFunc_JsonCache.Entities
{
    public class ApiToJsonEntity : TableEntity
    {
        public ApiToJsonEntity(string pPartitionKey, string pRowKey)
        {
            PartitionKey = pPartitionKey;
            RowKey = pRowKey;
        }
        
        public ApiToJsonEntity() { }

        public string JsonName { get; set; }
        public string Uri { get; set; }
        public string Container { get; set; }
    }

}