using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.UserModel
{
    public class User
    {
        public User(Guid? studentId, Guid? personId, Dictionary<string, string> accessRights)
        {
            StudentId = studentId;
            PersonId = personId;
            AccessRights = accessRights;
        }

        public User()
        {
            StudentId = Guid.Empty;
            PersonId = Guid.Empty;
            AccessRights = new Dictionary<string, string>();
        }

        public UInt64 Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? PersonId {  get; set; }
        public Dictionary<string, string> AccessRights { get; set; }
    }
}
