using Newtonsoft.Json;

namespace SmartSearch.App.Models
{
    public class Image
    {
        [JsonProperty(PropertyName = "data")]
        public required string Data { get; set; }
        [JsonProperty(PropertyName = "extension")]
        public required string Extension { get; set; }
    }
}
