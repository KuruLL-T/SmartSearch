using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.SearchItemTypeModel
{
    public class SearchItemType
    {
        public SearchItemType()
        {
            Name = "";
            ServiceDocumentId = 0;
            TypeId = Guid.Empty;
            Priority = 0;
            ServiceId = Guid.Empty;
        }

        public SearchItemType(string name, UInt64 serviceDocumentId, Guid typeId, Guid serviceId, int priority)
        {
            Name = name;
            ServiceDocumentId = serviceDocumentId;
            TypeId = typeId;
            Priority = priority;
            ServiceId = serviceId;
        }

        public UInt64 Id { get; set; }
        public string Name { get; set; }
        public UInt64 ServiceDocumentId { get; set; }
        public Guid TypeId {  get; set; }
        public int Priority { get; set; }
        public Guid ServiceId { get; set; }
    }
}
