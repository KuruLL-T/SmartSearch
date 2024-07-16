namespace SmartSearch.Api.Dto
{
    public class SearchResponseDto(List<SearchItemDto> items, int totalNumber)
    {
        public List<SearchItemDto> Items { get; set; } = items;
        public int TotalItemNumbers { get; set; } = totalNumber;
    }
}
