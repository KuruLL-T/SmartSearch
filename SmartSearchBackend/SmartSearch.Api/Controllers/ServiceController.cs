using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSearch.Api.Dto;
using SmartSearch.Api.Services;
using SmartSearch.Domain.ServiceModel;

namespace SmartSearch.Api.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly RedisService _redisService;
        public ServiceController(IServiceRepository serviceRepository, RedisService redisService)
        {
            _serviceRepository = serviceRepository;
            _redisService = redisService;
        }
        [HttpGet]
        [Route("get-allowed-services")]
        public async Task<IActionResult> GetAllowedServices() 
        {
            try
            {
                var accessRigths = await _redisService.GetDict("access_rights");
                List<ServiceDto> result = [];
                if (accessRigths.Count != 0)
                {
                    var allowedServices = accessRigths["servicesId"].Trim('[', ']').Split(',').ToList();
                    var services = await _serviceRepository.GetAllowedServices(allowedServices);
                    result = services.ConvertAll(x => new ServiceDto(x.Id.ToString(), x.Name));
                }
                else
                {
                    var services = await _serviceRepository.GetAll();
                    result = services.ConvertAll(x => new ServiceDto(x.Id.ToString(), x.Name));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
