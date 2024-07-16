using Newtonsoft.Json;
using System.Windows.Forms;

namespace SmartSearch.App.Models
{
    public class User : IModel
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; } = "user";
        [JsonProperty(PropertyName = "data")]
        public required UserData Data { get; set; }
        public override string ToString()
        {
            return $"{Data.ExternalId} {Type}";
        }
    }

    public class UserData
    {
        [JsonProperty(PropertyName = "userType")]
        public int UserType { get; set; }
        [JsonProperty(PropertyName = "externalId")]
        public Guid ExternalId { get; set; }
        [JsonProperty(PropertyName = "typeId")]
        public Guid TypeId { get; set; }
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
        public string AccessRights { get; set; }
    }
}
