using Newtonsoft.Json;

namespace SmartSearch.App.Models
{
    public class SearchItemTypeData
    {
        [JsonProperty(PropertyName = "name")]
        public required string Name { get; set; }
        [JsonProperty(PropertyName = "service_document_id")]
        public Guid ServiceDocumentId { get; set; }
        [JsonProperty(PropertyName = "type_id")]
        public Guid TypeId { get; set; }
        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }
        [JsonProperty(PropertyName = "service_id")]
        public Guid ServiceId { get; set; }
    }

    public class SearchItemType: IModel
    {
        [JsonProperty(PropertyName = "data")]
        public required SearchItemTypeData Data { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; } = "searchItemType";

        public override string ToString()
        {
            return $"{Data.Name}";
        }
    }
}
