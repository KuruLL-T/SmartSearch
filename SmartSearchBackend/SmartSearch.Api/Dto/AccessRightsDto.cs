namespace SmartSearch.Api.Dto
{
    public class AccessRightsDto
    {
        public string Key {  get; set; } = string.Empty;
        public Dictionary<string, string> AccessRigths { get; set; } = [];
    }
}
