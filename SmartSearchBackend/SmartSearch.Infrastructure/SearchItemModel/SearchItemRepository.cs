using ManticoreSearch.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartSearch.Domain.SearchItemModel;
using SmartSearch.Domain.ServiceModel;
using SmartSearch.Infrastructure.ManticoreProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Infrastructure.SearchItemModel
{
    public class SearchItemRepository : ISearchItemRepository
    {
        private readonly IManticoreClientProvider _manticoreProvicer;
        public SearchItemRepository(IManticoreClientProvider manticoreProvicer) 
        {
            _manticoreProvicer = manticoreProvicer;
        }
        public async Task<SearchItem> Add(SearchItem searchItem)
        {
            var indexApi = _manticoreProvicer.GetIndexApi();
            var doc = ConvertSearchItemToDict(searchItem);
            InsertDocumentRequest request = new(index: "search_item", doc: doc, id: (long)searchItem.Id);
            var response = await indexApi.InsertAsync(request);
            return new SearchItem
            {
                Id = (ulong)response.Id,
                Header = searchItem.Header,
                ExternalId = searchItem.ExternalId,
                Description = searchItem.Description,
                TypeId = searchItem.TypeId,
                ImgName = searchItem.ImgName,
                Link = searchItem.Link,
                AccessRights = searchItem.AccessRights,
            };
        }

        public async Task Delete(SearchItem searchItem)
        {
            var indexApi = _manticoreProvicer.GetIndexApi();
            var request = new DeleteDocumentRequest("search_item", id: (long)searchItem.Id);
            var response = await indexApi.DeleteAsync(request);
        }

        public async Task<SearchItem> GetById(ulong id)
        {
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM search_item where id = {id}", 
                true);
            var data = ResponseRawContentParser.Parse(response.RawContent);
            if (!data.HasValues)
            {
                throw new KeyNotFoundException($"there is no search_item with id = {id}");
            }
            var imgName = data[0]["img_name"].ToString();
            return new SearchItem
            {
                Id = id,
                Header = data[0]["header"].ToString(),
                Description = data[0]["description"].ToString(),
                TypeId = data[0]["type_id"].ToObject<ulong>(),
                ImgName = imgName == string.Empty ? null : imgName,
                Link = data[0]["link"].ToString(),
                AccessRights = JsonConvert.DeserializeObject<Dictionary<string, string>>(data[0]["access_rights"].ToString()),
            };            
        }

        public async Task Update(SearchItem searchItem)
        {
            var indexApi = _manticoreProvicer.GetIndexApi();
            var doc = ConvertSearchItemToDict(searchItem);
            var request = new InsertDocumentRequest(index: "search_item", id: (long)searchItem.Id, doc:doc);
            var response = await indexApi.ReplaceAsync(request);           
        }

        private static Dictionary<string, object> ConvertSearchItemToDict(SearchItem item)
        {
            Dictionary<string, object> result = [];
            result.Add("external_id", item.ExternalId);
            result.Add("header", item.Header);
            result.Add("description", item.Description);
            result.Add("type_id", item.TypeId);
            result.Add("img_name", item.ImgName ?? string.Empty);
            result.Add("link", item.Link);
            result.Add("access_rights", item.AccessRights == null ? "{}" : item.AccessRights);
            return result;
        }

        public async Task<SearchItem> GetByExternalId(Guid externalId)
        {
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM search_item WHERE external_id = \'{externalId}\'", true);
            var data = ResponseRawContentParser.Parse(response.RawContent);
            if (!data.HasValues)
            {
                throw new KeyNotFoundException($"there is no search_item with id = {externalId}");
            }
            var imgName = data[0]["img_name"].ToString();
            return new SearchItem
            {
                Id = data[0]["id"].ToObject<ulong>(),
                Header = data[0]["header"].ToString(),
                Description = data[0]["description"].ToString(),
                TypeId = data[0]["type_id"].ToObject<ulong>(),
                ImgName = imgName == string.Empty ? null : imgName,
                Link = data[0]["link"].ToString(),
                AccessRights = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    data[0]["access_rights"].ToString()),
            };
        }

        public async Task<Domain.SearchItemModel.SearchResponse> Search
            (Domain.SearchItemModel.SearchRequest request, Dictionary<string, string> accessRights)
        {
            string rawSqlRequest = "SELECT id, header, description, external_id, type_id, img_name, " +
                "link, access_rights, search_item_type.type_id, search_item_type.priority, " +
                "search_item_type.service_document_id, search_item_type.service_id FROM search_item " +
                "LEFT JOIN search_item_type ON search_item.type_id = search_item_type.id WHERE";
            switch (request.SearchTerm)
            {
                case SearchTerms.NONE:
                    {
                        rawSqlRequest = $"{rawSqlRequest} MATCH(\'@header {request.SearchString}\')";
                        break;
                    }
                case SearchTerms.BEGINS:
                    {
                        rawSqlRequest = $"{rawSqlRequest} MATCH(\'@header \"^{request.SearchString}\"\')";
                        break;
                    }
                case SearchTerms.CONTAINS:
                    {
                        rawSqlRequest = $"{rawSqlRequest} MATCH(\'@header \"{request.SearchString}\"\')";
                        break;
                    }
                case SearchTerms.EXCLUDES:
                    {
                        rawSqlRequest = $"{rawSqlRequest} MATCH(\'@header -\"{request.SearchString}\"\')";
                        break;
                    }
                default:
                    {
                        throw new Exception("There is no such search condition");
                    }
            }
            if (request.TypesId.Count != 0)
            {
                var filterParams = request.TypesId.ConvertAll(x => $"{x}");
                rawSqlRequest = $"{rawSqlRequest} AND {GetFilteredStringToQuery(filterParams, "type_id")}";
            }
            if (request.ServicesId.Count != 0)
            {
                var filterParams = request.ServicesId.ConvertAll(x => $"{x}");
                rawSqlRequest = $"{rawSqlRequest} AND {GetFilteredStringToQuery(filterParams, 
                    "search_item_type.service_document_id")}";
            }
            var jsonRights = JObject.Parse(ConvertDictToJsonString(accessRights));
            if (accessRights.Count != 0)
            {
                var servicesId = jsonRights["servicesId"]
                    .ToObject<List<string>>()
                    .ConvertAll(x => $"\'{x}\'");               
                rawSqlRequest = $"{rawSqlRequest} AND {GetFilteredStringToQuery(servicesId,
                    "search_item_type.service_id")}";
                var typesId = jsonRights["typesId"]
                    .ToObject<List<string>>()
                    .ConvertAll(x => $"\'{x}\'");
                rawSqlRequest = $"{rawSqlRequest} AND {GetFilteredStringToQuery(typesId,
                    "search_item_type.type_id")}";                
            }
            if (request.SearchTerm == SearchTerms.EXCLUDES)
            {
                rawSqlRequest = $"{rawSqlRequest} option not_terms_only_allowed=1";
            }
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            var response = await utilsApi.SqlWithHttpInfoAsync(rawSqlRequest, true);
            JArray data = (JArray)ResponseRawContentParser.Parse(response.RawContent);
            if (accessRights.Count == 0)
            {
                List<SearchResult> results = [];
                for (int i = request.Scipped; i < (data.Count > request.Scipped + 50 ?
                    request.Scipped + 50 : data.Count); i++)
                {
                    results.Add(ConvertJsonToSearchResult((JObject)data[i]));
                }
                return new Domain.SearchItemModel.SearchResponse(results, data.Count);
            }
            List<SearchResult> result = [];
            for (int i = 0; i < data.Count; i++)
            {                
                var rightsStr = data[i]["access_rights"].ToString();
                //if (rights.)
                if (rightsStr == "{}")
                //if (!data[i]["access_rights"].HasValues)
                {
                    result.Add(ConvertJsonToSearchResult((JObject)data[i]));
                    continue;
                }
                var rigtsForItem = JObject.Parse(data[i]["access_rights"].ToString());

                //var rigtsForItem = (JObject)data[i]["access_rights"];
                bool isAllowed = true;
                foreach (var keyValuePair in jsonRights)
                {
                    if (keyValuePair.Key == "servicesId" || keyValuePair.Key == "typesId")
                    {
                        continue;
                    }
                    if (!rigtsForItem.ContainsKey(keyValuePair.Key) ||
                        !CheckContainsJsonValueInArray(keyValuePair.Value,
                            rigtsForItem, keyValuePair.Key))
                    {
                        isAllowed = false;
                        break;
                        //if (!CheckContainsJsonValueInArray(keyValuePair.Value,
                        //    rigtsForItem, keyValuePair.Key))
                        //{
                        //    isAllowed = false;
                        //    break;
                        //}
                    }
                }
                if (isAllowed)
                {
                    result.Add(ConvertJsonToSearchResult((JObject)data[i]));

                }
            }       
            if (result.Count < 50)
            {
                return new Domain.SearchItemModel.SearchResponse(result, result.Count);
            }
            List<SearchResult> tmp = [];
            for (int i = request.Scipped; i < (result.Count > request.Scipped + 50 ?
                    request.Scipped + 50 : result.Count); i++)
            {
                tmp.Add(result[i]);
            }
            return new Domain.SearchItemModel.SearchResponse(tmp, result.Count);
        }

        private static bool CheckContainsJsonValueInArray(JToken jArray, JToken jObject, string key)
        {
            var values = jArray.ToObject<List<string>>();
            var value = jObject[key].ToString();
            if (value[0] == '[' && value[value.Length - 1] == ']')
            {
                value = value.Trim('[', ']');
                var tmpValues = value.Split(',').ToList().ConvertAll(x => x.Trim(' '));
                tmpValues = tmpValues.ConvertAll(x => x.Trim('\'', '\"'))
                    .OrderByDescending(x => x).ToList();
                values = values.ConvertAll(x => x.Trim(' '));
                values = values.ConvertAll(x => x.Trim('\'', '\"')).OrderByDescending(x => x)
                    .ToList();                
                return Enumerable.SequenceEqual(tmpValues, values);
            }
            value = value.Trim('\'', '\"', ' ');
            return values.Contains(value);
        }
        private static string GetFilteredStringToQuery(List<string> filterParams, string fieldForFilter)
        {
            string result = string.Empty;
            if (filterParams.Count == 1)
            {
                result = $"{fieldForFilter} = {filterParams[0]}";
            }
            else
            {
                result = $"{fieldForFilter} IN ({string.Join(',', filterParams)})";
            }
            return result;
        }
        private static SearchResult ConvertJsonToSearchResult(JObject json)
        {
            SearchItem item = new()
            {
                Id = json["id"].ToObject<ulong>(),
                Header = json["header"].ToString(),
                Description = json["description"].ToString(),
                ExternalId = json["external_id"].ToObject<Guid>(),
                TypeId = json["type_id"].ToObject<ulong>(),                
                ImgName = json["img_name"].ToString(),
                Link = json["link"].ToString(),
                AccessRights = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    json["access_rights"].ToString()),
            };
            //search_item_type.type_id
            //search_item_type.priority
            //search_item_type.service_document_id
            //"search_item_type.service_id"
            return new SearchResult(item, 
                json["search_item_type.service_document_id"].ToObject<ulong>(), 
                json["search_item_type.priority"].ToObject<int>());
        }
        private static string ConvertDictToJsonString(Dictionary<string, string> dict)
        {
            string result = "{\n";
            foreach (var key in dict.Keys)
            {
                result += $"\"{key}\": {dict[key]},\n";
            }
            result += "}";
            return result;
            //string result = "{"
        }
    }
}
