using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.SearchItemTypeModel
{
    public interface ISearchItemTypeRepository
    {
        Task<SearchItemType> Add(SearchItemType item);
        Task<SearchItemType> GetByName(string name);
        Task<SearchItemType> GetById(UInt64 id);
        Task<SearchItemType> GetByGuidId(Guid id);
        Task Update(SearchItemType item);
        Task Delete(SearchItemType item);
        Task<List<SearchItemType>> GetAllowedTypes(List<string> allowedTypesIds);
        Task<List<SearchItemType>> GetAll();
    }
}
