using ManticoreSearch.Client;
using SmartSearch.Domain.ServiceModel;
using SmartSearch.Infrastructure.ManticoreProvider;
using SmartSearch.Infrastructure.ServiceModel;
using System.Diagnostics;

namespace ManticoreRepositoriesTests
{
    [TestClass]
    public class ServiceRepositoryTests 
    {
        private  IManticoreClientProvider _provider;
        private  ServiceRepository _repository; 
        private List<Service> _services = [];
        public ServiceRepositoryTests()
        {
            _provider = new MockManticoreProvider("http://127.0.0.1:9308");
            _repository = new(_provider);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (Service service in _services)
            {
                await _repository.Delete(service);
            }
        }
        [TestMethod]
        public async Task TestAddindNonExistingService()
        {
            Service service = new()
            {
                ServiceId = Guid.NewGuid(),
                Name = "service123124"
            };
            var newService = await _repository.Add(service);
            _services.Add(newService);
            Assert.IsNotNull(newService.Id);            
            var tmp = await _repository.GetById(newService.Id);
            Assert.AreEqual(newService.Id, tmp.Id);
            Assert.AreEqual(newService.Name, tmp.Name);
            Assert.AreEqual(newService.ServiceId, tmp.ServiceId);
            //Task.WaitAll();
        }
        [TestMethod]
        public async Task TestAddingExistService()
        {
            Service service = new()
            {
                ServiceId = Guid.NewGuid(),
                Name = "service123124"
            };
            var newService = await _repository.Add(service);
            _services.Add(newService);
            await Assert.ThrowsExceptionAsync<ApiException>(() =>_repository.Add(newService));
        }
        [TestMethod]
        public async Task TestGettingByGuidId()
        {
            Service service = new()
            {
                ServiceId = Guid.NewGuid(),
                Name = "service123124"
            };
            var newService = await _repository.Add(service);
            _services.Add(newService);
            var tmp = await _repository.GetByGuidId(service.ServiceId);
            Assert.AreEqual(newService.Id, tmp.Id);
            Assert.AreEqual(newService.Name, tmp.Name);
            Assert.AreEqual(newService.ServiceId, tmp.ServiceId);
        }
        [TestMethod]
        public async Task TestGettingByGuidIdNonExistingService()
        {
            var id = Guid.NewGuid();
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _repository.GetByGuidId(id));        
        }
        [TestMethod]
        public async Task TestGettingByIdNonExistingService()
        {
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _repository.GetById(4218471214));
        }
        [TestMethod]
        public async Task TestGettingByNameExistingService()
        {
            Service service = new()
            {
                ServiceId = Guid.NewGuid(),
                Name = "service12"
            };
            var newService = await _repository.Add(service);
            _services.Add(newService);
            var tmp = await _repository.GetByName("service12");
            Assert.AreEqual(newService.Id, tmp.Id);
            Assert.AreEqual(newService.Name, tmp.Name);
            Assert.AreEqual(newService.ServiceId, tmp.ServiceId);
        }
        [TestMethod]
        public async Task TestGettingByNameSeverealService()
        {
            Service service = new()
            {
                ServiceId = Guid.NewGuid(),
                Name = "service123124"
            };
            var newService = await _repository.Add(service);
            var newService1 = await _repository.Add(service);
            await Assert.ThrowsExceptionAsync<Exception>(() => _repository.GetByName("service123124"));
            _services.Add(newService);
            _services.Add(newService1);
        }
        [TestMethod]
        public async Task TestGettingByNameNonExistingService()
        {
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _repository.GetByName("4218471214"));
        }
        [TestMethod]
        public async Task TestDeletingExistingService()
        {
            Service service = new()
            {
                ServiceId = Guid.NewGuid(),
                Name = "123124"
            };
            var newService = await _repository.Add(service);
            await _repository.Delete(service);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => 
            _repository.GetByGuidId(service.ServiceId));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => 
            _repository.GetById(service.Id));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() =>
            _repository.GetByName(service.Name));
        }
        [TestMethod]
        public async Task TestUpdateExistService()
        {
            Service service = new()
            {
                ServiceId = Guid.NewGuid(),
                Name = "123124"
            };
            var newService = await _repository.Add(service);
            newService.Name = "asfsgasgf";
            await _repository.Update(newService);
            var tmp = await _repository.GetById(newService.Id);
            Assert.AreEqual(newService.Name, tmp.Name);
            Assert.AreEqual(newService.ServiceId, tmp.ServiceId);
        }
        private void ServiceAssertion(Service first, Service second)
        {
            Assert.AreEqual(first.Id, second.Id);
            Assert.AreEqual(first.Name, second.Name);
            Assert.AreEqual(first.ServiceId, second.ServiceId);
        }
        [TestMethod]
        public async Task TestGetAllServices()
        {
            Service service1 = new("service1", Guid.NewGuid());
            Service service2 = new("service2", Guid.NewGuid());
            Service service3 = new("service3", Guid.NewGuid());
            service1 = await _repository.Add(service1);
            service2 = await _repository.Add(service2);
            service3 = await _repository.Add(service3);
            _services.AddRange([service1, service2, service3]);
            var tmpLst = await _repository.GetAll();
            Assert.AreEqual(tmpLst.Count, 3);
            var tmp1 = tmpLst.FirstOrDefault(x => x.Id == service1.Id);
            var tmp2 = tmpLst.FirstOrDefault(x => x.Id == service2.Id);
            var tmp3 = tmpLst.FirstOrDefault(x => x.Id == service3.Id);
            ServiceAssertion(service1, tmp1);
            ServiceAssertion(service2, tmp2);
            ServiceAssertion(service3, tmp3);
        }
        [TestMethod]
        public async Task TestGetAllowedServicesWhenSeverealAllowed()
        {
            Service service1 = new("service1", Guid.NewGuid());
            Service service2 = new("service2", Guid.NewGuid());
            Service service3 = new("service3", Guid.NewGuid());
            service1 = await _repository.Add(service1);
            service2 = await _repository.Add(service2);
            service3 = await _repository.Add(service3);
            _services.AddRange([service1, service2, service3]);
            var tmpLst = await _repository.GetAllowedServices([$"\'{service1.ServiceId}\'",
                $"\'{service3.ServiceId}\'"]);
            Assert.AreEqual(tmpLst.Count, 2);
            var tmp1 = tmpLst.FirstOrDefault(x => x.Id == service1.Id);
            var tmp2 = tmpLst.FirstOrDefault(x => x.Id == service2.Id);
            var tmp3 = tmpLst.FirstOrDefault(x => x.Id == service3.Id);
            Assert.IsNull(tmp2);
            ServiceAssertion(service1, tmp1);            
            ServiceAssertion(service3, tmp3);
        }
        [TestMethod]
        public async Task TestGetAllowedServicesWhenOneAllowed()
        {
            Service service1 = new("service1", Guid.NewGuid());
            Service service2 = new("service2", Guid.NewGuid());
            Service service3 = new("service3", Guid.NewGuid());
            service1 = await _repository.Add(service1);
            service2 = await _repository.Add(service2);
            service3 = await _repository.Add(service3);
            _services.AddRange([service1, service2, service3]);
            var tmpLst = await _repository.GetAllowedServices([$"\'{service1.ServiceId}\'"]);
            Assert.AreEqual(tmpLst.Count, 1);
            var tmp1 = tmpLst.FirstOrDefault(x => x.Id == service1.Id);
            var tmp2 = tmpLst.FirstOrDefault(x => x.Id == service2.Id);
            var tmp3 = tmpLst.FirstOrDefault(x => x.Id == service3.Id);
            Assert.IsNull(tmp2);
            Assert.IsNull(tmp3);
            ServiceAssertion(service1, tmp1);            
        }
    }
}
