using Newtonsoft.Json;

namespace SmartSearch.App.Models
{
    public interface IModel
    {
        public string Type { get; }
        [JsonIgnore]
        public static List<IModel> Models { get; set; } = [];
        public static void Save(IModel model)
        {
            Models.Add(model);
        }
    }
}
