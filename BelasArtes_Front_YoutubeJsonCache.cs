using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AzFunc_JsonCache.Function;
using AzFunc_JsonCache.Crosscutting.Interfaces;
using AzFunc_JsonCache.Crosscutting;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        public void Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Timer trigger function started at: {DateTime.Now}");

            // instanciando os serviços
            var storageService = new AzureStorageService(_config["AzureBlobConnectionString"]);
            var apiClient = new ApiClient();

            // dicionario das rotas a gerar ("nome do arquivo" => "uri da chamada")
            Dictionary<string, string> dicUris = new Dictionary<string, string>(){
                { "pba-channel.json", "https://www.googleapis.com/youtube/v3/search?key=AIzaSyCnr9msiJmpbRMQ6MMAMnBOo_5QjcUl--I&channelId=UCl8MCsaxzWnm_D4izigpTeg&part=snippet,id&order=date&maxResults=3&q=%23seucardápiosemanal" },
                { "pba-channel-list.json", "https://www.googleapis.com/youtube/v3/search?key=AIzaSyCnr9msiJmpbRMQ6MMAMnBOo_5QjcUl--I&channelId=UCl8MCsaxzWnm_D4izigpTeg&part=snippet,id&order=date&maxResults=15&q=%23seucardápiosemanal" }
            };

            ApiJsonCacheGenerator cacheGenerator = new ApiJsonCacheGenerator(log, storageService, apiClient);
            cacheGenerator.Generate(dicUris);
        }
    }
}
