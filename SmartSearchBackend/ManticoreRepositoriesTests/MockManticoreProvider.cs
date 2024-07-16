using ManticoreSearch.Api;
using ManticoreSearch.Client;
using Microsoft.Extensions.Configuration;
using SmartSearch.Infrastructure.ManticoreProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManticoreRepositoriesTests
{
    public class MockManticoreProvider : IManticoreClientProvider
    {
        private readonly Configuration _config;
        public MockManticoreProvider(string manticoreIpAddress) 
        {
            Configuration config = new Configuration();
            config.BasePath = manticoreIpAddress;
            _config = config;
        }
        public IndexApi GetIndexApi()
        {
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();            
            return new IndexApi(httpClient, _config, httpClientHandler);
        }

        public SearchApi GetSearchApi()
        {
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            return new SearchApi(httpClient, _config, httpClientHandler);
        }

        public UtilsApi GetUtilsApi()
        {
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            return new UtilsApi(httpClient, _config, httpClientHandler);
        }
    }
}
