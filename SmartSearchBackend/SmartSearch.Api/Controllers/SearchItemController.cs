using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSearch.Api.Dto;
using SmartSearch.Api.Services;
using SmartSearch.Domain.SearchItemModel;
using SmartSearch.Domain.UserModel;

namespace SmartSearch.Api.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class SearchItemController : ControllerBase
    {
        private readonly ISearchItemRepository _repositoty;
        private readonly ImageService _imageService;
        private readonly RedisService _redisService;
        private readonly IUserRepository _userRepository;
        public SearchItemController(ISearchItemRepository repository, ImageService imageService, 
            RedisService redisService, IUserRepository userRepository) 
        {
            _imageService = imageService;
            _repositoty = repository;
            _redisService = redisService;
            _userRepository = userRepository;
        }
        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto dto)
        {
            try
            {
                var item = new SearchItem(dto.TypeId, dto.ExternalId, dto.Header, dto.Description,
                    dto.ImgName, dto.Link, dto.AccessRights);
                item = await _repositoty.Add(item);
                bool isStud = false;
                if (dto.UserType == 0)
                {
                    isStud = true;
                }
                Guid? studentId = isStud ? dto.ExternalId : null;
                Guid? personId = isStud ? null : dto.ExternalId;
                var user = new User(studentId, personId, dto.AccessRights);
                user = await _userRepository.Add(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AddRequestDto dto)
        {
            try
            {
                var item = new SearchItem(dto.TypeId, dto.ExternalId, dto.Header, dto.Description,
                    dto.ImgName, dto.Link, dto.AccessRights);
                item = await _repositoty.Add(item);
                return Ok(item);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody] SearchRequestDto dto)
        {
            try
            {
                var request = new SearchRequest()
                {
                    SearchString = dto.SearchString,
                    SearchTerm = (SearchTerms)dto.SearchTerm,
                    Scipped = dto.Scipped,
                    ServicesId = dto.ServicesId.Select(s => ulong.Parse(s)).ToList(),
                    TypesId = dto.TypesId.Select(s => ulong.Parse(s)).ToList(),
                };
                var accessRights = await _redisService.GetDict("access_rights");
                var response = await _repositoty.Search(request, accessRights);
                var results = response.SearchResults.ConvertAll(x => new SearchItemDto(x.Item.Header,
                    x.Item.TypeId.ToString(), x.Item.ImgName, x.Priority, x.Item.Link));

                return Ok(new SearchResponseDto(results.ToList(), response.CountResults));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }
    }
}
