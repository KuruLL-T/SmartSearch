using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Domain.UserModel
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> GetByGuidId(Guid id);
        Task<User> GetById(UInt64 id);
        Task Update(User user);
        Task DeleteById(UInt64 id);
        Task DeleteByGuidId(Guid id);
        Task Delete(User user);
        Task<Dictionary<string, string>> GetAccessRightsByGuidId(Guid id);
    }
}
