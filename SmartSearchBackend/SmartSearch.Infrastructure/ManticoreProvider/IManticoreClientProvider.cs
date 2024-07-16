using System;
using System.Collections.Generic;
using System.Linq;
using ManticoreSearch.Api;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Infrastructure.ManticoreProvider
{
    public interface IManticoreClientProvider
    {
        UtilsApi GetUtilsApi();
        SearchApi GetSearchApi();
        IndexApi GetIndexApi();
    }
}
