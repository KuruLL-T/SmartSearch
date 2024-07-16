using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Infrastructure
{
    public static class ResponseRawContentParser
    {
        public static JToken Parse(string rawContent)
        {
            JObject jsonObject = JObject.Parse(rawContent.Trim(['[', ']']));
            var data = jsonObject["data"];
            return data;
           
        }
    }
}
