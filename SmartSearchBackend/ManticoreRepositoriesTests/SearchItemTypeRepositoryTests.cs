using ManticoreSearch.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSearch.Domain.SearchItemTypeModel;
using SmartSearch.Infrastructure.ManticoreProvider;
using SmartSearch.Infrastructure.SearchItemTypeModel;


namespace ManticoreRepositoriesTests
{
    public static class TypeAssertion
    {
        public static void AreTypesEqual(SearchItemType first, SearchItemType second)
        {
            Assert.AreEqual(first.Name, second.Name, "Type names are not equal");
            Assert.AreEqual(first.ServiceDocumentId, second.ServiceDocumentId, "Service document ids are not equal");
            Assert.AreEqual(first.TypeId, second.TypeId, "Type ids are not equal");
            Assert.AreEqual(first.Priority, second.Priority, "Priorities are not equal");
            Assert.AreEqual(first.ServiceId, first.ServiceId, "Service ids are not equal");
        }
    }

    [TestClass]
    public class SearchItemTypeRepositoryTests
    {
        private ISearchItemTypeRepository _searchItemTypeRepository;
        private List<SearchItemType> typesToDelete = new List<SearchItemType>();

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
                .AddScoped<ISearchItemTypeRepository, SearchItemTypeRepository>()
                .BuildServiceProvider();

            _searchItemTypeRepository = serviceProvider.GetRequiredService<ISearchItemTypeRepository>();
        }

        [TestMethod]
        public async Task Add_Type_In_Manticore()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var type = new SearchItemType("ученики", 5, guid, Guid.NewGuid(), 1);

            //Act
            var addedType = await _searchItemTypeRepository.Add(type);

            //Assert
            typesToDelete.Add(addedType);
            TypeAssertion.AreTypesEqual(addedType, type);
        }

        [TestMethod]
        public async Task Search_For_Non_Existent_Guid_Throws_Exception()
        {
            //Arrange
            var guid = Guid.NewGuid();

            //Act && Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _searchItemTypeRepository.GetByGuidId(guid),
                "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Search_For_Non_Existent_Name_Throws_Exception()
        {
            //Arrange
            var name = "unexisting_name";

            //Act && Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _searchItemTypeRepository.GetByName(name),
                "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Search_For_Non_Existent_Id_Throws_Exception()
        {
            //Arrange
            UInt64 id = 0;

            //Act && Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _searchItemTypeRepository.GetById(id),
                "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Update_Existing_Type()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var type = new SearchItemType("Ученики", 5, guid, Guid.NewGuid(), 1);
            var addedType = await _searchItemTypeRepository.Add(type);
            var newType = new SearchItemType
            {
                Id = addedType.Id,
                Name = "Кто здесь?",
                ServiceDocumentId = 52,
                TypeId = Guid.NewGuid(),
                Priority = 2,
                ServiceId = Guid.NewGuid(),
            };

            //Act
            await _searchItemTypeRepository.Update(newType);

            //Assert
            var updatedType = await _searchItemTypeRepository.GetById(newType.Id);
            typesToDelete.Add(updatedType);
            TypeAssertion.AreTypesEqual(newType, updatedType);
        }

        [TestMethod]
        public async Task Update_Non_Existing_Type()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var type = new SearchItemType
            {
                Id = 0,
                Name = "Ученики",
                ServiceDocumentId = 5,
                TypeId = guid,
                Priority = 2,
                ServiceId= Guid.NewGuid(),
            };

            //Act && Assert
            await Assert.ThrowsExceptionAsync<ApiException>(() => _searchItemTypeRepository.Update(type),
                 "An exception is expected to be thrown");
        }

        [TestMethod]
        public async Task Delete_Existing_Type()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var type = new SearchItemType("Ученики", 5, guid, Guid.NewGuid(),1);
            var addedType = await _searchItemTypeRepository.Add(type);

            //Act
            await _searchItemTypeRepository.Delete(addedType);

            //Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _searchItemTypeRepository.GetById(addedType.Id),
                "An exception is expected to be thrown");
        }

        /*[TestMethod]
        public async Task Get_Allowed_Types()
        {
            //Arrange
            List<Guid> allowedIds = [];
            List<SearchItemType> addedTypes = [];
            List<SearchItemType> expectedAllowedTypes = [];

            var guid = Guid.NewGuid();
            allowedIds.Add(guid);
            
            var type = new SearchItemType("Ученики", 5, guid, Guid.NewGuid(), 1);
            addedTypes.Add(await _searchItemTypeRepository.Add(type));
            expectedAllowedTypes.Add(addedTypes.Last());

            guid = Guid.NewGuid();
            type = new SearchItemType("Преподаватели", 4, guid, Guid.NewGuid(), 1);
            addedTypes.Add(await _searchItemTypeRepository.Add(type));

            guid = Guid.NewGuid();
            allowedIds.Add(guid);
            type = new SearchItemType("Какой-то тип", 4, guid, Guid.NewGuid(), 1);
            addedTypes.Add(await _searchItemTypeRepository.Add(type));
            expectedAllowedTypes.Add(addedTypes.Last());

            //Act
            var allowedTypes = await _searchItemTypeRepository.GetAllowedTypes(allowedIds);

            //Assert
            foreach (var addedType in addedTypes) 
            {
                typesToDelete.Add(addedType);
            }
            Assert.AreEqual(expectedAllowedTypes.Count, allowedTypes.Count, "The number of allowed types does not match");
            for (int i = 0; i < allowedTypes.Count; i++)
            {
                TypeAssertion.AreTypesEqual(expectedAllowedTypes[i], allowedTypes[i]);
            }
        }*/

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var type in typesToDelete)
            {
                _searchItemTypeRepository.Delete(type);
            }
        }
    }
}
