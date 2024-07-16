using SmartSearch.Domain.SearchItemTypeModel;
using SmartSearch.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.SearchItemModel
{
    public class SearchRequest
    {
        public string SearchString { get; set; } = string.Empty;
        public SearchTerms SearchTerm { get; set; } = SearchTerms.NONE;
        public List<ulong> ServicesId { get; set; }
        public List<ulong> TypesId { get; set; }
        public int Scipped { get; set; }

    }
}
