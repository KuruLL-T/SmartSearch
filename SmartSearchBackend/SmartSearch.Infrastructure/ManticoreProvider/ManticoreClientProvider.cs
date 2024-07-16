using ManticoreSearch.Api;
using ManticoreSearch.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Infrastructure.ManticoreProvider
{
    //Создаем HttpClient c помощью IHttpClientFactory
    public class ManticoreClientProvider : IManticoreClientProvider
    {
        private readonly Configuration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        public ManticoreClientProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            Configuration config = new();
            config.BasePath = configuration.GetConnectionString("ManticoreConnection");
            _config = config;
        }
        public IndexApi GetIndexApi()
        {            
            var httpClient = _httpClientFactory.CreateClient();
            var indexApi = new IndexApi(httpClient, _config);            
            return indexApi;
        }

        public SearchApi GetSearchApi()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var searchApi = new SearchApi(httpClient, _config);
            return searchApi;
        }

        public UtilsApi GetUtilsApi()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var utilsApi = new UtilsApi(httpClient, _config);
            return utilsApi;
        }
    }
}
