namespace SmartSearch.Api.Dto
{
    public class SearchItemDto(string name, string typeId, string img, int priority, string link)
    {
        public string Name { get; set; } = name;
        public string TypeId { get; set; } = typeId;
        public string Base64Img { get; set; } = img;
        public int Priority {  get; set; } = priority;
        public string Link {  get; set; } = link;
    }
}
