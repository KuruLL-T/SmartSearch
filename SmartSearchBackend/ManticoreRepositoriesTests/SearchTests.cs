using SmartSearch.Domain.SearchItemModel;
using SmartSearch.Domain.SearchItemTypeModel;
using SmartSearch.Domain.ServiceModel;
using SmartSearch.Infrastructure.ManticoreProvider;
using SmartSearch.Infrastructure.SearchItemModel;
using SmartSearch.Infrastructure.SearchItemTypeModel;
using SmartSearch.Infrastructure.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManticoreRepositoriesTests
{
    [TestClass]
    public class SearchTests
    {
        private IManticoreClientProvider _provider;
        private SearchItemRepository _itemRepository;
        private SearchItemTypeRepository _typeRepository;
        private ServiceRepository _serviceRepository;
        private List<SearchItem> _items = [];
        private List<Service> _sevices = [];
        private List<SearchItemType> _types = [];
        public SearchTests()
        {
            _provider = new MockManticoreProvider("http://127.0.0.1:9308");
            _itemRepository = new(_provider);
            _typeRepository = new(_provider);
            _serviceRepository = new(_provider);
        }
        [TestInitialize]
        public async Task TestInit()
        {
            Service service1 = new("service1", Guid.NewGuid());
            Service service2 = new("service2", Guid.NewGuid());
            service1 = await _serviceRepository.Add(service1);
            service2 = await _serviceRepository.Add(service2);
            _sevices.Add(service1);
            _sevices.Add(service2);
            SearchItemType type1 = new("type1", service1.Id, Guid.NewGuid(), service1.ServiceId, 1);
            SearchItemType type2 = new("type2", service1.Id, Guid.NewGuid(), service1.ServiceId, 1);
            SearchItemType type3 = new("type3", service2.Id, Guid.NewGuid(), service2.ServiceId, 1);
            type1 = await _typeRepository.Add(type1);
            type2 = await _typeRepository.Add(type2);
            type3 = await _typeRepository.Add(type3);

            _types.AddRange([type1, type2, type3]);
            SearchItem item1 = new(type1.Id, Guid.NewGuid(), "header", "descr", "qwert", "zxccv", []);
            SearchItem item2 = new(type1.Id, Guid.NewGuid(), "descr", "header", "hkgk", "zxccv", []);
            SearchItem item3 = new(type2.Id, Guid.NewGuid(), "descr asd", "zxc header ", "hkgk", "zxccv", []);
            SearchItem item4 = new(type2.Id, Guid.NewGuid(), "zxc header ", " asd descr  ", "hkgk", "zxccv", []);
            SearchItem item5 = new(type3.Id, Guid.NewGuid(), "poilkh", " mnjui", "hkgk", "zxccv", []);
            _items.AddRange([item1, item2, item3, item4, item5]);
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i] = await _itemRepository.Add(_items[i]);
            }
            int a = 8;
        }
        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var service in _sevices)
            {
                await _serviceRepository.Delete(service);
            }          
        }
        private static void AssertSearchResult(SearchResult first,  SearchResult second)
        {
            Assert.AreEqual(first.Item.Id, second.Item.Id);
            Assert.AreEqual(first.Item.Header, second.Item.Header);
            Assert.AreEqual(first.Item.Description, second.Item.Description);
            Assert.AreEqual(first.Item.ImgName, second.Item.ImgName);
            Assert.AreEqual(first.Item.TypeId, second.Item.TypeId);
            Assert.AreEqual(first.Item.ExternalId, second.Item.ExternalId);
            Assert.AreEqual(first.Item.Link, second.Item.Link);
            Assert.AreEqual(System.Linq.Enumerable.SequenceEqual(first.Item.AccessRights, second.Item.
                AccessRights), true);
            Assert.AreEqual(first.ServiceId, second.ServiceId);
            Assert.AreEqual(first.Priority, second.Priority);

        }
        [TestMethod]
        public async Task TestSearhWithoutTermsAndAccessToOneTypeAndService()
        {
            SearchRequest request = new()
            {
                SearchString = "header",
                SearchTerm = SearchTerms.NONE,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            Dictionary<string, string> accessRights = [];
            accessRights.Add("servicesId", $"[\'{_sevices[0].ServiceId}\']");
            accessRights.Add("typesId", $"[\'{_types[0].TypeId}\']");
            var result = await _itemRepository.Search(request, accessRights);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CountResults, 1);
            var tmp = new SearchResult(_items[0], _sevices[0].Id, 1);
            AssertSearchResult(result.SearchResults[0], tmp);
        }
        [TestMethod]
        public async Task TestSearhWithoutTermsAndAllTypesAndService()
        {
            SearchRequest request = new()
            {
                SearchString = "header",
                SearchTerm = SearchTerms.NONE,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            var result = await _itemRepository.Search(request, []);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CountResults, 2);
            var tmp1 = new SearchResult(_items[0], _sevices[0].Id, 1);
            var tmp2 = new SearchResult(_items[3], _sevices[0].Id, 1);
            List<SearchResult> tmpLst = [tmp1, tmp2];
            tmpLst = tmpLst.OrderByDescending(x => x.Item.Id).ToList();
            result.SearchResults = result.SearchResults.OrderByDescending(x => x.Item.Id).ToList();
            for (int i = 0; i < tmpLst.Count; i++)
            {
                AssertSearchResult(tmpLst[i], result.SearchResults[i]);
            }
        }
        [TestMethod]
        public async Task TestSearchWithBeginsTerms()
        {
            SearchRequest request = new()
            {
                SearchString = "header",
                SearchTerm = SearchTerms.BEGINS,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            var result = await _itemRepository.Search(request, []);            
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CountResults, 1);
            var tmp = new SearchResult(_items[0], _sevices[0].Id, 1);
            AssertSearchResult(result.SearchResults[0], tmp);
        }
        [TestMethod]
        public async Task TestSearchWithContainsTerms()
        {
            SearchRequest request = new()
            {
                SearchString = "header",
                SearchTerm = SearchTerms.CONTAINS,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            var result = await _itemRepository.Search(request, []);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CountResults, 2);
            var tmp1 = new SearchResult(_items[0], _sevices[0].Id, 1);
            var tmp2 = new SearchResult(_items[3], _sevices[0].Id, 1);
            List<SearchResult> tmpLst = [tmp1, tmp2];
            tmpLst = tmpLst.OrderByDescending(x => x.Item.Id).ToList();
            result.SearchResults = result.SearchResults.OrderByDescending(x => x.Item.Id).ToList();
            for (int i = 0; i < tmpLst.Count; i++)
            {
                AssertSearchResult(tmpLst[i], result.SearchResults[i]);
            }

        }
        [TestMethod]
        public async Task TestSearchWithExcludesTerms()
        {
            SearchRequest request = new()
            {
                SearchString = "header",
                SearchTerm = SearchTerms.EXCLUDES,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            var result = await _itemRepository.Search(request, []);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CountResults, 3);
            var tmp1 = new SearchResult(_items[1], _sevices[0].Id, 1);
            var tmp2 = new SearchResult(_items[2], _sevices[0].Id, 1);
            var tmp3 = new SearchResult(_items[4], _sevices[1].Id, 1);
            List<SearchResult> tmpLst = [tmp1, tmp2, tmp3];
            tmpLst = tmpLst.OrderByDescending(x => x.Item.Id).ToList();
            result.SearchResults = result.SearchResults.OrderByDescending(x => x.Item.Id).ToList();
            for (int i = 0; i < tmpLst.Count; i++)
            {
                AssertSearchResult(tmpLst[i], result.SearchResults[i]);
            }
        }
        [TestMethod]
        public async Task TestSearchWithAccessRightsWhenHaveAccessToAllElement()
        {
            Dictionary<string, string> rights1 = [];            
            rights1.Add("facultyId", Guid.NewGuid().ToString());
            rights1.Add("groupId", Guid.NewGuid().ToString());
            Dictionary<string, string> rights2 = [];
            rights2.Add("facultyId", Guid.NewGuid().ToString());
            rights2.Add("groupId", Guid.NewGuid().ToString());
            SearchItem item1 = new(_types[0].Id, Guid.NewGuid(), "test 1", "descr", "qwert", "zxccv", rights1);
            SearchItem item2 = new(_types[0].Id, Guid.NewGuid(), "test 2", "header", "hkgk", "zxccv", rights2);
            item1 = await _itemRepository.Add(item1);
            item2 = await _itemRepository.Add(item2);
            _items.AddRange([item1, item2]);
            Dictionary<string, string> userRights = [];
            userRights.Add("servicesId", $"[\'{_sevices[0].ServiceId}\', '{_sevices[1].ServiceId}']");
            userRights.Add("typesId", $"[\'{_types[0].TypeId}\', '{_types[1].TypeId}', '{_types[2].TypeId}']");
            userRights.Add("facultyId", $"[\"{rights1["facultyId"]}\", \"{rights2["facultyId"]}\"]");
            userRights.Add("groupId", $"[\"{rights1["groupId"]}\", \"{rights2["groupId"]}\"]");
            SearchRequest request = new()
            {
                SearchString = "test",
                SearchTerm = SearchTerms.NONE,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            var response = await _itemRepository.Search(request, userRights);
            var tmp1 = new SearchResult(item1, _sevices[0].Id, 1);
            var tmp2 = new SearchResult(item2, _sevices[0].Id, 1);
            List<SearchResult> tmpLst = [tmp1, tmp2];
            tmpLst = tmpLst.OrderByDescending(x => x.Item.Id).ToList();
            response.SearchResults = response.SearchResults.OrderByDescending(x => x.Item.Id).ToList();
            for (int i = 0; i < tmpLst.Count; i++)
            {
                AssertSearchResult(tmpLst[i], response.SearchResults[i]);
            }
        }
        [TestMethod]
        public async Task TestSearchWithAccessRightsWhenHaveAccessToNotAllElements()
        {
            Dictionary<string, string> rights1 = [];
            rights1.Add("facultyId", Guid.NewGuid().ToString());
            rights1.Add("groupId", Guid.NewGuid().ToString());
            Dictionary<string, string> rights2 = [];
            rights2.Add("facultyId", Guid.NewGuid().ToString());
            rights2.Add("groupId", Guid.NewGuid().ToString());
            SearchItem item1 = new(_types[0].Id, Guid.NewGuid(), "test 1", "descr", "qwert", "zxccv", rights1);
            SearchItem item2 = new(_types[0].Id, Guid.NewGuid(), "test 2", "header", "hkgk", "zxccv", rights2);
            item1 = await _itemRepository.Add(item1);
            item2 = await _itemRepository.Add(item2);
            _items.AddRange([item1, item2]);
            Dictionary<string, string> userRights = [];
            userRights.Add("servicesId", $"[\'{_sevices[0].ServiceId}\', '{_sevices[1].ServiceId}']");
            userRights.Add("typesId", $"[\'{_types[0].TypeId}\', '{_types[1].TypeId}', '{_types[2].TypeId}']");
            userRights.Add("facultyId", $"[\"{rights1["facultyId"]}\"]");
            userRights.Add("groupId", $"[\"{rights1["groupId"]}\", \"{rights2["groupId"]}\"]");            
            SearchRequest request = new()
            {
                SearchString = "test",
                SearchTerm = SearchTerms.NONE,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            var response = await _itemRepository.Search(request, userRights);
            Assert.AreEqual(response.CountResults, 1);
            var tmp1 = new SearchResult(item1, _sevices[0].Id, 1);           
            List<SearchResult> tmpLst = [tmp1];
            tmpLst = tmpLst.OrderByDescending(x => x.Item.Id).ToList();
            response.SearchResults = response.SearchResults.OrderByDescending(x => x.Item.Id).ToList();
            for (int i = 0; i < tmpLst.Count; i++)
            {
                AssertSearchResult(tmpLst[i], response.SearchResults[i]);
            }
        }
        [TestMethod]
        public async Task TestSearchWithAccessRightsWhenHaveAccessToAnotherUser()
        {
            Dictionary<string, string> rights1 = [];
            rights1.Add("facultyId", $"[\"{Guid.NewGuid()}\", \"{Guid.NewGuid()}\"]");                     
            SearchItem item1 = new(_types[0].Id, Guid.NewGuid(), "test 1", "descr", "qwert", "zxccv", rights1);            
            item1 = await _itemRepository.Add(item1);

            _items.AddRange([item1]);
            Dictionary<string, string> userRights = [];
            userRights.Add("servicesId", $"[\'{_sevices[0].ServiceId}\', '{_sevices[1].ServiceId}']");
            userRights.Add("typesId", $"[\'{_types[0].TypeId}\', '{_types[1].TypeId}', '{_types[2].TypeId}']");
            userRights.Add("facultyId", $"{rights1["facultyId"]}");           
            SearchRequest request = new()
            {
                SearchString = "test",
                SearchTerm = SearchTerms.NONE,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            var response = await _itemRepository.Search(request, userRights);
            Assert.AreEqual(response.CountResults, 1);
            var tmp1 = new SearchResult(item1, _sevices[0].Id, 1);
            List<SearchResult> tmpLst = [tmp1];
            tmpLst = tmpLst.OrderByDescending(x => x.Item.Id).ToList();
            response.SearchResults = response.SearchResults.OrderByDescending(x => x.Item.Id).ToList();
            for (int i = 0; i < tmpLst.Count; i++)
            {
                AssertSearchResult(tmpLst[i], response.SearchResults[i]);
            }
        }
        [TestMethod]
        public async Task TestSearchWithAccessRightsWhenHaveAccessToAnotherUserAndNotUser()
        {
            var tmpGuid = Guid.NewGuid();
            Dictionary<string, string> rights1 = [];
            rights1.Add("facultyId", $"[\"{tmpGuid}\", \"{Guid.NewGuid()}\"]");
            SearchItem item1 = new(_types[0].Id, Guid.NewGuid(), "test 1", "descr", "qwert", "zxccv", rights1);           
            Dictionary<string, string> rights2 = [];
            rights2.Add("facultyId", $"{tmpGuid}");
            SearchItem item2 = new(_types[0].Id, Guid.NewGuid(), "test 2", "descr", "qwert", "zxccv", rights2);
            item1 = await _itemRepository.Add(item1);
            item2 = await _itemRepository.Add(item2);
            _items.AddRange([item1, item2]);
            Dictionary<string, string> userRights = [];
            userRights.Add("servicesId", $"[\'{_sevices[0].ServiceId}\', '{_sevices[1].ServiceId}']");
            userRights.Add("typesId", $"[\'{_types[0].TypeId}\', '{_types[1].TypeId}', '{_types[2].TypeId}']");
            userRights.Add("facultyId", $"{rights1["facultyId"]}");
            SearchRequest request = new()
            {
                SearchString = "test",
                SearchTerm = SearchTerms.NONE,
                Scipped = 0,
                ServicesId = [],
                TypesId = []
            };
            var response = await _itemRepository.Search(request, userRights);            
            var tmp1 = new SearchResult(item1, _sevices[0].Id, 1);
            var tmp2 = new SearchResult(item2, _sevices[0].Id, 1);
            List<SearchResult> tmpLst = [tmp1, tmp2];
            tmpLst = tmpLst.OrderByDescending(x => x.Item.Id).ToList();
            response.SearchResults = response.SearchResults.OrderByDescending(x => x.Item.Id).ToList();
            for (int i = 0; i < tmpLst.Count; i++)
            {
                AssertSearchResult(tmpLst[i], response.SearchResults[i]);
            }
        }
    }
}
