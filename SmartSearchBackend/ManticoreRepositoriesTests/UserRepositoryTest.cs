using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSearch.Domain.UserModel;
using SmartSearch.Infrastructure.ManticoreProvider;
using SmartSearch.Infrastructure.UserModel;

namespace ManticoreRepositoriesTests
{
    static class UserAssertion
    {
        public static void AreUsersEqual(User first, User second)
        {
            Assert.AreEqual(first.StudentId, second.StudentId, "Student id are not equal");
            Assert.AreEqual(first.PersonId, second.PersonId, "Person id are not equal");
            Assert.AreEqual(System.Linq.Enumerable.SequenceEqual(first.AccessRights, second.AccessRights), true, "Access rights are not equal");
        }
    }
    [TestClass]
    public class UserRepositoryTest
    {
        private IUserRepository _userRepository;
        private List<Guid> userGuids = new List<Guid>();

        [TestInitialize]
        public void Initialize()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:ManticoreConnection", "http://localhost:9308"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddHttpClient()
                .AddSingleton<IManticoreClientProvider, ManticoreClientProvider>()
                .AddScoped<IUserRepository, UserRepository>()
                .BuildServiceProvider();

            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
        }

        [TestMethod]
        public async Task Add_User_To_Manticore_With_Person_Id()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            userGuids.Add(id);

            var user = new User(null, id, new Dictionary<string, string> {
                { "group", "PS-33" },
                { "faculty", "FIVT" }
            });

            //Act
            var addedUser = await _userRepository.Add(user);

            //Assert
            UserAssertion.AreUsersEqual(addedUser, user);
        }

        [TestMethod]
        public async Task Add_User_To_Manticore_With_Student_Id()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            userGuids.Add(id);

            var user = new User(id, null, new Dictionary<string, string> {
                { "group", "PS-33" },
                { "faculty", "FIVT" }
            });

            //Act
            var addedUser = await _userRepository.Add(user);

            //Assert
            UserAssertion.AreUsersEqual(addedUser, user);
        }

        [TestMethod]
        public async Task Add_User_To_Manticore_With_Student_Id_And_Person_Id_Throws_Exception()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var user = new User(id, id, new Dictionary<string, string> {
                { "group", "PS-33" },
                { "faculty", "FIVT" }
            });

            //Act && Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _userRepository.Add(user),
                "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Add_User_To_Manticore_Without_Student_Id_And_Person_Id_Throws_Exception()
        {
            //Arrange
            var user = new User(null, null, new Dictionary<string, string> {
                { "group", "PS-33" },
                { "faculty", "FIVT" }
            });

            //Act && Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _userRepository.Add(user),
                "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Delete_User_From_Manticore_With_Id()
        {
            //Arrange
            var id = Guid.NewGuid();

            var user = new User(id, null, new Dictionary<string, string> {
                { "group", "PS-33" },
                { "faculty", "FIVT" }
            });

            var addedUser = await _userRepository.Add(user);

            //Act
            await _userRepository.DeleteById(addedUser.Id);

            //Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await _userRepository.GetByGuidId(id),
                "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Delete_User_From_Manticore_With_Guid()
        {
            //Arrange
            var id = Guid.NewGuid();

            var user = new User(id, null, new Dictionary<string, string> {
                { "group", "PS-33" },
                { "faculty", "FIVT" }
            });

            var addedUser = await _userRepository.Add(user);

            //Act
            await _userRepository.DeleteByGuidId(id);

            //Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await _userRepository.GetByGuidId(id), 
                "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Delete_User_From_Manticore_With_Full_Parameters()
        {
            //Arrange
            var id = Guid.NewGuid();

            var user = new User(id, null, new Dictionary<string, string> {
                { "group", "PS-33" },
                { "faculty", "FIVT" }
            });

            var addedUser = await _userRepository.Add(user);

            //Act
            await _userRepository.Delete(addedUser);

            //Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await _userRepository.GetByGuidId(id),
                "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Update_User_In_Manticore()
        {
            //Arrange
            var id = Guid.NewGuid();
            Console.WriteLine($"старый студент айди {id}");

            var user = new User(id, null, new Dictionary<string, string> {
                { "group", "PS-33" },
                { "faculty", "FIVT" }
            });

            var newId = Guid.NewGuid();

            userGuids.Add(newId);

            var addedUser = await _userRepository.Add(user);

            var newUser = new User(null, newId, new Dictionary<string, string> {
                { "group", "BI-14" },
                { "faculty", "BEBRA" }
            });
            newUser.Id = addedUser.Id;

            //Act
            await _userRepository.Update(newUser);

            //Assert
            var updatedUser = await _userRepository.GetByGuidId(newId);
            UserAssertion.AreUsersEqual(updatedUser, newUser);
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach(var guid in userGuids)
            {
                _userRepository.DeleteByGuidId(guid);
            }
        }
    }
}
