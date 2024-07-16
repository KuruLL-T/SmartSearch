using ManticoreSearch.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartSearch.Domain.SearchItemTypeModel;
using SmartSearch.Domain.UserModel;
using SmartSearch.Infrastructure.ManticoreProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Infrastructure.SearchItemTypeModel
{
    public class SearchItemTypeRepository : ISearchItemTypeRepository
    {
        private readonly IManticoreClientProvider _manticoreClientProvider;

        public SearchItemTypeRepository(IManticoreClientProvider manticoreClientProvider)
        {
            _manticoreClientProvider = manticoreClientProvider;
        }

        public async Task<SearchItemType> Add(SearchItemType item)
        {
            var indexApi = _manticoreClientProvider.GetIndexApi();

            var document = new Dictionary<string, object>
            {
                { "name", item.Name },
                { "service_document_id", item.ServiceDocumentId },
                { "type_id", item.TypeId },
                { "priority", item.Priority},
                { "service_id", item.ServiceId.ToString() }
            };

            var newDoc = new InsertDocumentRequest(
                index: "search_item_type",
                id: 0,
                doc: document
            );

            var response = await indexApi.InsertAsync(newDoc);

            return new SearchItemType
            {
                Id = (UInt64)response.Id,
                Name = item.Name,
                ServiceDocumentId = item.ServiceDocumentId,
                TypeId = item.TypeId,
                Priority = item.Priority,
                ServiceId = item.ServiceId,
            };
        }

        public async Task Delete(SearchItemType item)
        {
            var utilsApi = _manticoreClientProvider.GetUtilsApi();
            var indexApi = _manticoreClientProvider.GetIndexApi();
            var rawSqlRequest = $"DELETE FROM search_item WHERE type_id = {item.Id}";
            await utilsApi.SqlWithHttpInfoAsync(rawSqlRequest, true);
            DeleteDocumentRequest deleteRequest = new DeleteDocumentRequest(
               index: "search_item_type",
               id: (long)item.Id
               );
            await indexApi.DeleteAsync(deleteRequest);
        }

        public async Task<SearchItemType> GetByGuidId(Guid id)
        {
            var utilsApi = _manticoreClientProvider.GetUtilsApi();

            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM search_item_type WHERE type_id = \'{id}\'", true);

            var dataList = ResponseRawContentParser.Parse(response.RawContent);

            if (!dataList.HasValues)
            {
                throw new KeyNotFoundException($"There is no type with type id = {id}");
            }

            var data = dataList[0];

            return new SearchItemType
            {
                Id = data["id"].ToObject<UInt64>(),
                Name = data["name"].ToString(),
                ServiceDocumentId = data["service_document_id"].ToObject<UInt64>(),
                TypeId = Guid.Parse(data["type_id"].ToString()),
                Priority = data["priority"].ToObject<int>(),
                ServiceId = Guid.Parse(data["service_id"].ToString())
            };
        }

        public async Task<SearchItemType> GetById(ulong id)
        {
            var utilsApi = _manticoreClientProvider.GetUtilsApi();

            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM search_item_type WHERE id = {id}", true);

            var dataList = ResponseRawContentParser.Parse(response.RawContent);

            if (!dataList.HasValues)
            {
                throw new KeyNotFoundException($"There is no type with id = {id}");
            }

            var data = dataList[0];

            return new SearchItemType
            {
                Id = data["id"].ToObject<UInt64>(),
                Name = data["name"].ToString(),
                ServiceDocumentId = data["service_document_id"].ToObject<UInt64>(),
                TypeId = Guid.Parse(data["type_id"].ToString()),
                Priority = data["priority"].ToObject<int>(),
                ServiceId = Guid.Parse(data["service_id"].ToString())
            };
        }

        public async Task<SearchItemType> GetByName(string name)
        {
            var utilsApi = _manticoreClientProvider.GetUtilsApi();

            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM search_item_type WHERE name = \'{name}\'", true);

            var dataList = ResponseRawContentParser.Parse(response.RawContent);

            if (!dataList.HasValues)
            {
                throw new KeyNotFoundException($"There is no type with name = {name}");
            }

            var data = dataList[0];

            return new SearchItemType
            {
                Id = data["id"].ToObject<UInt64>(),
                Name = data["name"].ToString(),
                ServiceDocumentId = data["service_document_id"].ToObject<UInt64>(),
                TypeId = Guid.Parse(data["type_id"].ToString()),
                Priority = data["priority"].ToObject<int>(),
                ServiceId = Guid.Parse(data["service_id"].ToString())
            };
        }

        public async Task<List<SearchItemType>> GetAllowedTypes(List<string> allowedTypesIds)
        {
            List<SearchItemType> result = [];
            var utilsApi = _manticoreClientProvider.GetUtilsApi();
            string rawRequest = "SELECT * FROM search_item_type WHERE type_id";
            if (allowedTypesIds.Count == 1)
            {
                rawRequest = $"{rawRequest} = {allowedTypesIds[0]}";
            }
            else
            {
                rawRequest = $"{rawRequest} IN ({string.Join(',', allowedTypesIds)})";
            }
            var response = await utilsApi.SqlWithHttpInfoAsync(rawRequest, true);
            var data = (JArray)ResponseRawContentParser.Parse(response.RawContent);
            foreach (var item in data)
            {
                result.Add(new SearchItemType
                {
                    Id = item["id"].ToObject<UInt64>(),
                    Name = item["name"].ToString(),
                    ServiceDocumentId = item["service_document_id"].ToObject<UInt64>(),
                    TypeId = Guid.Parse(item["type_id"].ToString()),
                    Priority = item["priority"].ToObject<int>(),
                    ServiceId = Guid.Parse(item["service_id"].ToString())
                });
            }
            /*List<SearchItemType> result = [];
            var utilsApi = _manticoreClientProvider.GetUtilsApi();
            var rawRequest = "SELECT * FROM search_item_type";
            var response = await utilsApi.SqlWithHttpInfoAsync(rawRequest, true);
            var data = (JToken)ResponseRawContentParser.Parse(response.RawContent);

            foreach ( var item in data) 
            {
                var typeId = item["type_id"].ToString();
                if (allowedTypesIds.Contains(Guid.Parse(typeId)))
                {
                    result.Add(new SearchItemType
                    {
                        Id = item["id"].ToObject<UInt64>(),
                        Name = item["name"].ToString(),
                        ServiceDocumentId = item["service_document_id"].ToObject<UInt64>(),
                        TypeId = Guid.Parse(item["type_id"].ToString()),
                        Priority = item["priority"].ToObject<int>(),
                        ServiceId = Guid.Parse(item["service_id"].ToString())
                    });
                }
            }*/

            return result;
        }

        public async Task<List<SearchItemType>> GetAll()
        {
            List<SearchItemType> result = [];
            var utilsApi = _manticoreClientProvider.GetUtilsApi();
            var rawRequest = $"SELECT * FROM search_item_type";
            var response = await utilsApi.SqlWithHttpInfoAsync(rawRequest, true);
            var data = (JArray)ResponseRawContentParser.Parse(response.RawContent);
            foreach (var item in data)
            {
                result.Add(new SearchItemType
                {
                    Id = item["id"].ToObject<UInt64>(),
                    Name = item["name"].ToString(),
                    ServiceDocumentId = item["service_document_id"].ToObject<UInt64>(),
                    TypeId = Guid.Parse(item["type_id"].ToString()),
                    Priority = item["priority"].ToObject<int>(),
                    ServiceId = Guid.Parse(item["service_id"].ToString())
                });
            }
            return result;
        }

        public async Task Update(SearchItemType item)
        {
            var indexApi = _manticoreClientProvider.GetIndexApi();

            var document = new Dictionary<string, object>
            {
                { "name", item.Name },
                { "service_document_id", item.ServiceDocumentId },
                { "priority", item.Priority },
                { "service_id", item.ServiceId },
                { "type_id", item.TypeId },
            };

            var updateRequest = new UpdateDocumentRequest(
                index: "search_item_type",
                id: (long)item.Id,
                doc: document
                );

            indexApi.Update(updateRequest);
        }
    }
}
