using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using AzFunc_JsonCache.Crosscutting.Interfaces;
using AzFunc_JsonCache.Crosscutting;
using AzFunc_JsonCache.Entities;

namespace AzFunc_JsonCache.Function
{
    public class ApiJsonCacheGenerator
    {
        ILogger _log;
        IApiClient _apiClient;
        IStorageService _storageService;

        public ApiJsonCacheGenerator(ILogger log, IStorageService storageService, IApiClient apiClient)
        {
            this._log = log;
            this._storageService = storageService;
            this._apiClient = apiClient;
        }

        public void Generate(List<ApiToJsonEntity> uris)
        {
            foreach (ApiToJsonEntity uri in uris)
            {
                // a chave do dicionario é o nome do arquivo destino no blob
                // enquanto o valor é a url a dar um get
                string url = uri.Uri;
                string pathDestiny = $"{uri.JsonName}.json";

                Task.Run(async () => {
                    try {
                        string apiResponse = await this._apiClient.Get(url);
                        string pathSaved = await this._storageService.SendTextContent(
                            apiResponse,
                            pathDestiny,
                            uri.Container
                        );
                        this._log.LogInformation($"Cached:{pathDestiny} -> {url} -> {pathSaved}. (at: {DateTime.Now})");
                    } 
                    catch(Exception ex)
                    {
                        this._log.LogError($"Exception in {uri.Uri}/{uri.JsonName} => {ex.Message} ({DateTime.Now})");
                    }
                });                    
            }
        }
    }
}