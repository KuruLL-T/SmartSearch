using ManticoreSearch.Api;
using ManticoreSearch.Client;
using ManticoreSearch.Model;
using Microsoft.Extensions.Configuration;
using SmartSearch.Domain.SearchItemTypeModel;
using System.Diagnostics;

namespace SmartSearch.Infrastructure
{
    public static class DbInitializer
    {        
        private static async Task CreateTable(UtilsApi utilsApi, string tableName, string attributes)
        {
            var response = await utilsApi
                    .SqlWithHttpInfoAsync($"SHOW TABLES LIKE \'{tableName}\'", true);
            var rawResponse = response.RawContent;
            if (!rawResponse.Contains(tableName))
            {
                await utilsApi
                    .SqlAsync($"CREATE TABLE {tableName}({attributes})", true);
            }
        }
        public static async void Initialize(IConfiguration configuration)
        {

            Configuration config = new Configuration();
            config.BasePath = configuration.GetConnectionString("ManticoreConnection");
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UtilsApi(httpClient, config, httpClientHandler);            
            await CreateTable(apiInstance, "search_item_type",
                "name string, service_document_id bigint, priority int, service_id string, type_id string");
            await CreateTable(apiInstance, "service", "name string, service_id string");
            await CreateTable(apiInstance, "search_item",
                "external_id string, type_id bigint, header text, description text, img_name string, link string, access_rights json");
            await CreateTable(apiInstance, 
                "user", "student_id string, person_id string, access_rights json");                                       
        }
    }
}
