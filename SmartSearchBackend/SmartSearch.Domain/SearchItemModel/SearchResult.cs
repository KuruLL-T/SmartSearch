using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.SearchItemModel
{
    public class SearchResult(SearchItem item, UInt64 serviceId, int priority)
    {
        public SearchItem Item { get; set; } = item;
        public UInt64 ServiceId { get; set; } = serviceId;
        public int Priority { get; set; } = priority;
    }

    public class SearchResponse(List<SearchResult> searchResults, int countResults)
    {
        public List<SearchResult> SearchResults { get; set; } = searchResults;
        public int CountResults {  get; set; } = countResults;
    }

}
