using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzFunc_JsonCache.Entities
{
    public class ApiToJsonRepository {
        public static async Task<List<ApiToJsonEntity>> GetAll() {
            var storageAccount = CloudStorageAccount
                .Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
 
            var table = storageAccount
                .CreateCloudTableClient().GetTableReference(Environment.GetEnvironmentVariable("TableApiToJson"));
 
            var query = new TableQuery<ApiToJsonEntity>().Where(
                TableQuery.GenerateFilterCondition(
                    "PartitionKey", 
                    QueryComparisons.Equal, 
                    Environment.GetEnvironmentVariable("PartitionKey")
                )
            );

            List<ApiToJsonEntity> listAcaoEntity = new List<ApiToJsonEntity>();
            TableContinuationToken continuationToken = null;

            var items = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
            return items.Results;
        }
    }
}