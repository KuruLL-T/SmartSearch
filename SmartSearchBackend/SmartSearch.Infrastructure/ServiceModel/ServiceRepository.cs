using SmartSearch.Domain.ServiceModel;
using SmartSearch.Domain.SearchItemTypeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSearch.Infrastructure.ManticoreProvider;
using ManticoreSearch.Model;
using Newtonsoft.Json.Linq;

namespace SmartSearch.Infrastructure.ServiceModel
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IManticoreClientProvider _manticoreProvicer;
        public ServiceRepository(IManticoreClientProvider manticoreProvicer)
        {
            _manticoreProvicer = manticoreProvicer;
        }
        public async Task<Service> Add(Service service)
        {
            var indexApi = _manticoreProvicer.GetIndexApi();
            Dictionary<string, object> doc = ConvertServiceToDict(service);
            InsertDocumentRequest request = new(index: "service", doc: doc, id: (long)service.Id);
            var response =  await indexApi.InsertAsync(request);
            return new Service
            {
                Id = (ulong)response.Id,
                Name = service.Name,
                ServiceId = service.ServiceId
            };
        }

        public async Task Delete(Service service)
        {
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            var indexApi = _manticoreProvicer.GetIndexApi();
            var request = new DeleteDocumentRequest("service", id: (long)service.Id);
            var rawSqlRequest = $"SELECT id FROM search_item_type WHERE service_document_id = {service.Id}";
            var response = await utilsApi.SqlWithHttpInfoAsync(rawSqlRequest, true);
            var data = (JArray)ResponseRawContentParser.Parse(response.RawContent);
            if (data.Count == 0)
            {
                await indexApi.DeleteAsync(request);
                return;
            }
            List<string> typesId = [];            
            foreach (var item in data )
            {
                typesId.Add(item["id"].ToString());
            }
            rawSqlRequest = $"DELETE FROM search_item WHERE type_id IN ({string.Join(',', typesId)})"; 
                //$"DELETE FROM search_item_type WHERE id IN ({string.Join(',', typesId)})";
            response = await utilsApi.SqlWithHttpInfoAsync(rawSqlRequest, true);
            rawSqlRequest = $"DELETE FROM search_item_type WHERE id IN ({string.Join(',', typesId)})";
            response = await utilsApi.SqlWithHttpInfoAsync(rawSqlRequest, true);
            await indexApi.DeleteAsync(request);
        }

        public async Task<Service> GetByGuidId(Guid id)
        {
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM service WHERE service_id = \'{id}\'", true);
            var data = ResponseRawContentParser.Parse(response.RawContent);
            if (!data.HasValues)
            {
                throw new KeyNotFoundException($"there is no service with service_id = {id}");
            }     
            return new Service
            {
                Id = data[0]["id"].ToObject<ulong>(),
                Name = data[0]["name"].ToString(),
                ServiceId = id
            };
        }

        public async Task<Service> GetById(ulong id)
        {
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM service WHERE id = {id}", true);
            var data = ResponseRawContentParser.Parse(response.RawContent);
            if (!data.HasValues)
            {
                throw new KeyNotFoundException($"there is no service with id = {id}");
            }
            return new Service
            {
                Id = id,
                Name = data[0]["name"].ToString(),
                ServiceId = new Guid(data[0]["service_id"].ToString())
            };
        }

        public async Task<Service> GetByName(string name)
        {
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM service WHERE name = \'{name}\'", true);
            var data = ResponseRawContentParser.Parse(response.RawContent);
            
            if (!data.HasValues)
            {
                throw new KeyNotFoundException($"there is no service with name = {name}");
            }
            if (data.Count() > 1)
            {
                throw new Exception($"there are several service with name = {name}");
            }
            return new Service
            {
                Id = data[0]["id"].ToObject<ulong>(),
                Name = name,
                ServiceId = new Guid(data[0]["service_id"].ToString())
            };
        }

        public async Task Update(Service service)
        {
            var indexApi = _manticoreProvicer.GetIndexApi();
            var doc = ConvertServiceToDict(service);
            var request = new InsertDocumentRequest(index: "service", doc: doc, id: (long)service.Id);
            var response = await indexApi.ReplaceAsync(request);
        }

        private static Dictionary<string, object> ConvertServiceToDict(Service service)
        {
            Dictionary<string, object> result = [];
            result.Add("name", service.Name);
            result.Add("service_id", service.ServiceId);
            return result;
        }

        public async Task<List<Service>> GetAllowedServices(List<string> allowedServicesIds)
        {
            List<Service> result = [];
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            string rawRequest = "SELECT * FROM service WHERE service_id";
            if (allowedServicesIds.Count == 1)
            {
                rawRequest = $"{rawRequest} = {allowedServicesIds[0]}";
            }
            else
            {
                rawRequest = $"{rawRequest} IN ({string.Join(',', allowedServicesIds)})";
            }
            var response = await utilsApi.SqlWithHttpInfoAsync(rawRequest, true);
            var data = (JArray)ResponseRawContentParser.Parse(response.RawContent);
            foreach (var item in data)
            {
                result.Add(new Service
                    {
                        Id = item["id"].ToObject<ulong>(),
                        Name = item["name"].ToString(),
                        ServiceId = new Guid(item["service_id"].ToString())
                    });
            }
            //foreach (var item in data)
            //{
            //    var serviceId = item["service_id"].ToString();
            //    if (allowedServicesIds.Contains($"\"{serviceId}\""))
            //    {
            //        result.Add(new Service
            //        {
            //            Id = item["id"].ToObject<ulong>(),
            //            Name = item["name"].ToString(),
            //            ServiceId = new Guid(item["service_id"].ToString())
            //        });
            //    }
            //}
            return result;            
        }

        public async Task<List<Service>> GetAll()
        {
            List<Service> result = [];
            var utilsApi = _manticoreProvicer.GetUtilsApi();
            var rawRequest = $"SELECT * FROM service";
            var response = await utilsApi.SqlWithHttpInfoAsync(rawRequest, true);
            var data = (JArray)ResponseRawContentParser.Parse(response.RawContent);
            foreach (var item in data)
            {
                result.Add(new Service
                {
                    Id = item["id"].ToObject<ulong>(),
                    Name = item["name"].ToString(),
                    ServiceId = new Guid(item["service_id"].ToString())
                });
            }
            return result;
        }
    }
}
