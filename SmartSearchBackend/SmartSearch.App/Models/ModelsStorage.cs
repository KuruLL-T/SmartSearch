namespace SmartSearch.App.Models
{
    public class ModelsStorage
    {
        public static List<User> users { get; set; } = new List<User>();
        public static List<Service> services { get; set; } = new List<Service>();
        public static List<SearchItem> searchItems { get; set; } = new List<SearchItem>();
        public static List<SearchItemType> searchItemTypes { get; set; } = new List<SearchItemType>();
    }
}
