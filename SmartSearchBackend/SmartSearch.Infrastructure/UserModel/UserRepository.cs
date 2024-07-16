using ManticoreSearch.Model;
using Newtonsoft.Json;
using SmartSearch.Domain.UserModel;
using SmartSearch.Infrastructure.ManticoreProvider;

namespace SmartSearch.Infrastructure.UserModel
{
    public class UserRepository : IUserRepository
    {
        private readonly IManticoreClientProvider _manticoreClientProvider;

        public UserRepository(IManticoreClientProvider manticoreClientProvider)
        {
            _manticoreClientProvider = manticoreClientProvider;
        }

        public async Task<User> Add(User user)
        {
            var indexApi = _manticoreClientProvider.GetIndexApi();

            var document = new Dictionary<string, object> 
            {
                { "access_rights", JsonConvert.SerializeObject(user.AccessRights) }
            };

            if (user.StudentId == null && user.PersonId == null
                || user.StudentId != null && user.PersonId != null)
            {
                throw new ArgumentException("There must be one guid");
            }

            if (user.StudentId != null)
                document.Add("student_id", user.StudentId);
            if (user.PersonId != null)
                document.Add("person_id", user.PersonId);

            var newDoc = new InsertDocumentRequest(
                index: "user",
                id: 0,
                doc: document
                );

            var response = await indexApi.InsertAsync( newDoc );

            return new User
            {
                Id = (UInt64)response.Id,
                StudentId = user.StudentId,
                PersonId = user.PersonId,
                AccessRights = user.AccessRights
            };
        }

        public async Task DeleteByGuidId(Guid id)
        {
            var indexApi = _manticoreClientProvider.GetIndexApi();

            var user = await GetByGuidId(id);

            await DeleteById((ulong)user.Id);
        }

        public async Task DeleteById(UInt64 id)
        {
            var indexApi = _manticoreClientProvider.GetIndexApi();

            DeleteDocumentRequest deleteRequest = new DeleteDocumentRequest(
                index: "user",
                id: (long)id
                );

            await indexApi.DeleteAsync( deleteRequest );
        }

        public async Task Delete(User user)
        {
            var indexApi = _manticoreClientProvider.GetIndexApi();

            DeleteDocumentRequest deleteRequest = new DeleteDocumentRequest(
                index: "user",
                id: (long)user.Id
                );

            await indexApi.DeleteAsync(deleteRequest);
        }

        public async Task<User> GetByGuidId(Guid id)
        {
            var utilsApi = _manticoreClientProvider.GetUtilsApi();

            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM user WHERE student_id = \'{id}\' OR person_id = \'{id}\'", true);

            var dataList = ResponseRawContentParser.Parse(response.RawContent);

            if (!dataList.HasValues)
            {
                throw new KeyNotFoundException($"There is no user with guid = {id}");
            }

            var data = dataList[0];

            return new User
            {
                Id = data["id"].ToObject<UInt64>(),
                StudentId = data["student_id"].ToString() == "" ? null : Guid.Parse(data["student_id"].ToString()),
                PersonId = data["person_id"].ToString() == "" ? null : Guid.Parse(data["person_id"].ToString()),
                AccessRights = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["access_rights"].ToString())
            };
        }

        public async Task<User> GetById(ulong id)
        {
            var utilsApi = _manticoreClientProvider.GetUtilsApi();

            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT * FROM user WHERE id = \'{id}\'", true);

            var dataList = ResponseRawContentParser.Parse(response.RawContent);

            if (!dataList.HasValues)
            {
                throw new KeyNotFoundException($"There is no user with id = {id}");
            }

            var data = dataList[0];

            return new User
            {
                Id = data["id"].ToObject<UInt64>(),
                StudentId = data["student_id"].ToString() == "" ? null : Guid.Parse(data["student_id"].ToString()),
                PersonId = data["person_id"].ToString() == "" ? null : Guid.Parse(data["person_id"].ToString()),
                AccessRights = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["access_rights"].ToString())
            };
        }

        public async Task<Dictionary<string, string>> GetAccessRightsByGuidId(Guid id)
        {
            var utilsApi = _manticoreClientProvider.GetUtilsApi();

            var response = await utilsApi.SqlWithHttpInfoAsync($"SELECT access_rights FROM user WHERE student_id = \'{id}\' OR person_id = \'{id}\'", true);

            var dataList = ResponseRawContentParser.Parse(response.RawContent);

            if (!dataList.HasValues)
            {
                throw new KeyNotFoundException($"There is no user with guid = {id}");
            }

            var data = dataList[0];

            var accessRights = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["access_rights"].ToString());

            return accessRights;
        }

        public async Task Update(User user)
        {
            var indexApi = _manticoreClientProvider.GetIndexApi();

            var document = new Dictionary<string, object>
            {
                { "access_rights", JsonConvert.SerializeObject(user.AccessRights) }
            };

            if (user.StudentId == null && user.PersonId == null
                || user.StudentId != null && user.PersonId != null)
            {
                throw new ArgumentException("There must be one guid");
            }

            if (user.StudentId != null)
                document.Add("student_id", user.StudentId);
            else
                document.Add("student_id", "");

            if (user.PersonId != null)
                document.Add("person_id", user.PersonId);
            else
                document.Add("person_id", "");

            var updateRequest = new UpdateDocumentRequest(
                index: "user",
                id: (long)user.Id,
                doc: document            
                );

            indexApi.Update(updateRequest);
        }
    }
}
