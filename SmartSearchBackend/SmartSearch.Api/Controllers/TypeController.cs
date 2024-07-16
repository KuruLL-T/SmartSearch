using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartSearch.Api.Dto;
using SmartSearch.Api.Services;
using SmartSearch.Domain.SearchItemTypeModel;
using SmartSearch.Domain.UserModel;

namespace SmartSearch.Api.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class TypeController : ControllerBase
    {
        private readonly ISearchItemTypeRepository _searchItemTypeRepository;
        private readonly RedisService _redisService;
        private readonly IUserRepository _userRepository;
        public TypeController(ISearchItemTypeRepository searchItemTypeRepository, RedisService redisService, IUserRepository userRepository) 
        {
            _searchItemTypeRepository = searchItemTypeRepository;
            _redisService = redisService;
            _userRepository = userRepository;
        }

        [HttpGet("get-allowed-types")]
        public async Task<IActionResult> GetAllowedTypes() 
        {
            try
            {
                var accessRigths = await _redisService.GetDict("access_rights");
                List<TypeDto> result = [];
                if (accessRigths.Count != 0)
                {
                    var allowedTypes = accessRigths["typesId"].Trim('[', ']').Split(',').ToList();
                    var types = await _searchItemTypeRepository.GetAllowedTypes(allowedTypes);
                    result = types.ConvertAll(x => new TypeDto(x.Id.ToString(), x.Name, x.ServiceDocumentId.ToString()));
                }
                else
                {
                    var types = await _searchItemTypeRepository.GetAll();
                    result = types.ConvertAll(x => new TypeDto(x.Id.ToString(), x.Name, x.ServiceDocumentId.ToString()));
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
