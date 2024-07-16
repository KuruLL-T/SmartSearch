using Newtonsoft.Json;

namespace SmartSearch.App.Models
{
    public class ServiceData
    {
        [JsonProperty(PropertyName = "service_id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public required string Name { get; set; }
    }

    public class Service: IModel
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; } = "service";
        [JsonProperty(PropertyName = "data")]
        public required ServiceData Data { get; set; }

        public override string ToString()
        {
            return $"{Data.Name}";
        }
    }
}
