using Newtonsoft.Json;

namespace SmartSearch.App.Models
{
    public class SearchItem : IModel
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; } = "searchItem";
        [JsonProperty(PropertyName = "data")]
        public required SearchItemData Data { get; set; }

        public override string ToString()
        {
            return $"{Data.ExternalId} {Data.Header}";
        }
    }

    public class SearchItemData
    {
        [JsonProperty(PropertyName = "type_id")]
        public Guid TypeId { get; set; }
        [JsonProperty(PropertyName = "external_id")]
        public Guid ExternalId { get; set; }
        [JsonProperty(PropertyName = "header")]
        public required string Header { get; set; }
        [JsonProperty(PropertyName = "description")]
        public required string Description { get; set; }
        [JsonIgnore]
        public required Image Image { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string ImageString { get; set; }
        [JsonProperty(PropertyName = "link")]
        public required string Link { get; set; }
        [JsonProperty(PropertyName = "access_rights")]
        public required string AccessRights { get; set; }
    }
}
