namespace SmartSearch.Api.Dto
{
    public class SearchRequestDto
    {
        public string SearchString {  get; set; }
        public string[] ServicesId {  get; set; }
        public string[] TypesId { get; set; }
        public int SearchTerm { get; set; }
        public int Scipped {  get; set; }
    }
}
