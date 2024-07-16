using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.ServiceModel
{
    public class Service
    {
        public Service(string name, Guid serviceId)
        {
            Name = name;
            ServiceId = serviceId;
        }
        public Service()
        {
            Id = 0;
            Name = string.Empty;
            ServiceId = Guid.Empty;
        }
        public UInt64 Id { get; set; }
        public string Name { get; set; }
        public Guid ServiceId { get; set; }
    }
}
