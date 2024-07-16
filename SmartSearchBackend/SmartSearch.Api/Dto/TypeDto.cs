namespace SmartSearch.Api.Dto
{
    public class TypeDto(string id, string name, string serviceId)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string ServiceId { get; set; } = serviceId;
    }
}
