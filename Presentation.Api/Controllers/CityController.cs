using System;
using System.Threading.Tasks;
using Application.DTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddCityAsync([FromBody] AddCityRequestDto postalCodeDto)
        {
            var resultDto = await CityService.AddCityAsync(postalCodeDto.PostalCode);

            if (!resultDto.Success.Value) return BadRequest(resultDto);

            return Ok(resultDto);
        }

        [HttpDelete]
        [Route("{key}")]
        public async Task<IActionResult> RemoveAsync([FromRoute]Guid key)
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

        [HttpPost]
        [Route("{key}/temperatures")]
        public async Task<IActionResult> AddTemperatureAsync([FromRoute] Guid key, [FromBody] AddTemperatureRequestDto temperatureRequestDto)
        {
            var result = await CityService.AddTemperatureAsync(key, temperatureRequestDto);

            if (!result.Success.Value) return BadRequest(result);
            return Ok(result);
        }
    }
}