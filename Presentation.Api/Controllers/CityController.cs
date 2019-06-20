using Application.DTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        ICityService CityService { get; }

        public CityController(ICityService cityService)
        {
            CityService = cityService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddCity([FromBody] AddCityRequestDto postalCodeDto)
        {
            var resultDto = await CityService.AddCityAsync(postalCodeDto.PostalCode);

            if (!resultDto.Success.Value) return BadRequest(resultDto);

            return Ok(resultDto);
        }

        [HttpDelete]
        [Route("{key}")]
        public async Task<IActionResult> RemoveAsync(string key)
        {
            var resultDto = await CityService.RemoveAsync(key);

            if (!resultDto.Success.Value) return BadRequest(resultDto);

            return Ok(resultDto);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PagingDto pagingDto)
        {
            var resultDto = await CityService.GetAllAsync(pagingDto);
            return Ok(resultDto);
        }
    }
}