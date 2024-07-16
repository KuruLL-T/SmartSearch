using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSearch.Api.Dto;
using SmartSearch.Api.Services;

namespace SmartSearch.Api.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class AccessRigthsController : ControllerBase
    {
        private readonly RedisService _redisService;
        public AccessRigthsController(RedisService redisService)
        {
            _redisService = redisService;
        }
        [HttpPost]
        [Route("set-access-rigths")]
        public async Task<IActionResult> SetAccessRights([FromBody] AccessRightsDto dto)
        {
            try
            {
                await _redisService.SetDict(dto.Key, dto.AccessRigths);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
