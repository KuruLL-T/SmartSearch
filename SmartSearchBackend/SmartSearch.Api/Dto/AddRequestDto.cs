namespace SmartSearch.Api.Dto
{
    public class AddRequestDto
    {
        public UInt64 TypeId { get; set; }
        public Guid ExternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string? ImgName { get; set; }
        public string Link { get; set; }
        public Dictionary<string, string> AccessRights { get; set; }

    }
}
