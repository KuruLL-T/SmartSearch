using SmartSearch.Domain.SearchItemTypeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.ServiceModel
{
    public interface IServiceRepository
    {
        Task<Service> Add(Service service);
        Task Delete(Service service);
        Task Update(Service service);
        Task<Service> GetByName(string name);
        Task<Service> GetById(UInt64 id);
        Task<Service> GetByGuidId(Guid id);
        Task<List<Service>> GetAllowedServices(List<string> allowedServicesIds);
        Task<List<Service>> GetAll();
    }
}
