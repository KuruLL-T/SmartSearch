using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.SearchItemModel
{
    public interface ISearchItemRepository
    {
        Task<SearchItem> Add(SearchItem searchItem);
        Task<SearchResponse> Search(SearchRequest request, Dictionary<string, string> accessRights); 
        Task Update(SearchItem searchItem);
        Task Delete(SearchItem searchItem);
        Task<SearchItem> GetByExternalId(Guid externalId);        
        Task<SearchItem> GetById(UInt64 id);
    }
}
