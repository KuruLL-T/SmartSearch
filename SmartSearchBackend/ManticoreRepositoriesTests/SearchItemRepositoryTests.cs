using ManticoreSearch.Client;
using SmartSearch.Domain.SearchItemModel;
using SmartSearch.Domain.ServiceModel;
using SmartSearch.Domain.UserModel;
using SmartSearch.Infrastructure.ManticoreProvider;
using SmartSearch.Infrastructure.SearchItemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManticoreRepositoriesTests
{
    [TestClass]
    public class SearchItemRepositoryTests
    {
        private IManticoreClientProvider _provider;
        private SearchItemRepository _repository;
        private List<SearchItem> _items = [];
        public SearchItemRepositoryTests()
        {
            _provider = new MockManticoreProvider("http://127.0.0.1:9308");
            _repository = new(_provider);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (SearchItem item in _items)
            {
                await _repository.Delete(item);
            }
        }
        [TestMethod]
        public async Task TestAddingNonExistingItem()
        {
            Dictionary<string, string> accessRights = [];
            accessRights.Add("faculty_id", Guid.NewGuid().ToString());
            var item = new SearchItem(44252, Guid.NewGuid(),
                "header", "decrt qwe", "asfsaf", "asfer322", accessRights);
            var newItem = await _repository.Add(item);            
            _items.Add(newItem);
            Assert.IsNotNull(newItem.Id);;
            var tmp = await _repository.GetByExternalId(item.ExternalId);
            Assert.AreEqual(newItem.Header, tmp.Header);
            Assert.AreEqual(newItem.Description, tmp.Description);
            Assert.AreEqual(newItem.ImgName, tmp.ImgName);
            Assert.AreEqual(newItem.TypeId, tmp.TypeId);
            Assert.AreEqual(newItem.Link, tmp.Link);
            Assert.AreEqual(System.Linq.Enumerable.SequenceEqual(newItem.AccessRights, tmp.AccessRights), true);
        }
        [TestMethod]
        public async Task TestAddingExistItem()
        {
            Dictionary<string, string> accessRights = [];
            accessRights.Add("faculty_id", Guid.NewGuid().ToString());
            var item = new SearchItem(44252, Guid.NewGuid(),
                "header", "decrt", "asfsaf", "asfer322", accessRights);
            var newItem = await _repository.Add(item);
            _items.Add(newItem);
            await Assert.ThrowsExceptionAsync<ApiException>(() => _repository.Add(newItem));
        }
        [TestMethod]
        public async Task TestUpdateItem()
        {
            Dictionary<string, string> accessRights = [];
            accessRights.Add("faculty_id", Guid.NewGuid().ToString());
            var item = new SearchItem(44252, Guid.NewGuid(),
                "header", "decrt", "asfsaf", "asfer322", accessRights);
            var newItem = await _repository.Add(item);
            _items.Add(newItem);
            newItem.Header = "asdf";
            newItem.TypeId = 678;
            await _repository.Update(newItem);
            var tmp = await _repository.GetById(newItem.Id);
            Assert.AreEqual(newItem.Header, tmp.Header);
            Assert.AreEqual(newItem.Description, tmp.Description);
            Assert.AreEqual(newItem.ImgName, tmp.ImgName);
            Assert.AreEqual(newItem.TypeId, tmp.TypeId);
            Assert.AreEqual(newItem.Link, tmp.Link);
            Assert.AreEqual(System.Linq.Enumerable.SequenceEqual(newItem.AccessRights, tmp.AccessRights), true);
        }
        [TestMethod]
        public async Task TestGetByIdNonExistItem()
        {
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _repository.GetById(4218471214));
        }
        [TestMethod]
        public async Task TestGetByGuidIdNonExistItem()
        {
            var id = Guid.NewGuid();
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _repository.GetByExternalId(id));
        }
        [TestMethod]
        public async Task TestGetByGuidIdExistItem()
        {
            Dictionary<string, string> accessRights = [];
            accessRights.Add("faculty_id", Guid.NewGuid().ToString());
            var item = new SearchItem(44252, Guid.NewGuid(),
                "header", "decrt", "asfsaf", "asfer322", accessRights);
            var newItem = await _repository.Add(item);
            _items.Add(newItem);
            var tmp = await _repository.GetByExternalId(newItem.ExternalId);
            Assert.AreEqual(newItem.Header, tmp.Header);
            Assert.AreEqual(newItem.Description, tmp.Description);
            Assert.AreEqual(newItem.ImgName, tmp.ImgName);
            Assert.AreEqual(newItem.TypeId, tmp.TypeId);
            Assert.AreEqual(newItem.Link, tmp.Link);
            Assert.AreEqual(System.Linq.Enumerable.SequenceEqual(newItem.AccessRights, tmp.AccessRights), true);
        }
        [TestMethod]
        public async Task TestDeleteItem()
        {
            Dictionary< string, string> accessRights = [];
            accessRights.Add("faculty_id", Guid.NewGuid().ToString());
            var item = new SearchItem(44252, Guid.NewGuid(),
                "header", "decrt", "asfsaf", "asfer322", accessRights);
            var newItem = await _repository.Add(item);
            await _repository.Delete(newItem);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _repository.GetById(4218471214));
        }
    }
}
