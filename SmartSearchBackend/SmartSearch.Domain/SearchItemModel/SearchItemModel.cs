using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.SearchItemModel
{
    public class SearchItem
    {
        public SearchItem(UInt64 typeId, Guid externalId, string header, string description,
        string? imgName, string link, Dictionary<string, string> accessRights)
        {
            TypeId = typeId;
            Header = header;
            ExternalId = externalId;
            Description = description;
            ImgName = imgName;
            Link = link;
            AccessRights = accessRights;
        }
        public SearchItem()
        {
            Id = 0;
            TypeId = 0;
            Header = string.Empty;
            ExternalId = Guid.Empty;
            Description = string.Empty;
            ImgName = null;
            Link = string.Empty;
            AccessRights = [];
        }
        public UInt64 Id { get; set; }        
        public UInt64 TypeId { get; set; }
        public Guid ExternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string? ImgName { get; set; }
        public string Link { get; set; }
        public Dictionary<string, string> AccessRights { get; set; }
    }
}
