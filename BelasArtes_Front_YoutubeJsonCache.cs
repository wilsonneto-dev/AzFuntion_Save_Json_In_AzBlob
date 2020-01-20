using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AzFunc_JsonCache.Function;
using AzFunc_JsonCache.Crosscutting;

using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AzFunc_JsonCache.Entities;

namespace Enc.BelasArtes_Youtube_JsonCache
{
    public class BelasArtes_Front_YoutubeJsonCache
    {
        private IConfiguration _config { get; set; }
        
        public BelasArtes_Front_YoutubeJsonCache(IConfiguration config)
        {
            this._config = config;
        }

        /* public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        */

        [FunctionName("BelasArtes_Front_YoutubeJsonCache")]
        public async Task Run([TimerTrigger("0 0 */6 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Timer trigger function started at: {DateTime.Now}");

            // instanciando os serviços
            var storageService = new AzureStorageService(_config["AzureBlobConnectionString"]);
            var apiClient = new ApiClient();

            List<ApiToJsonEntity> listApiToJson = await ApiToJsonRepository.GetAll();

            ApiJsonCacheGenerator cacheGenerator = new ApiJsonCacheGenerator(log, storageService, apiClient);
            cacheGenerator.Generate(listApiToJson);
        }
    }
}
