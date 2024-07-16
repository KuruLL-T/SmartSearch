using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartSearch.Api.Dto
{
    public class ServiceDto(string id, string name)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
    }
}
