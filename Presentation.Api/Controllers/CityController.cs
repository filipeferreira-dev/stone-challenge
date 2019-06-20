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
            var result = await CityService.AddCityAsync(postalCodeDto.PostalCode);

            if (result.Success.HasValue && !result.Success.Value) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{key}")]
        public async Task<IActionResult> RemoveAsync([FromRoute]Guid key)
        {
            var result = await CityService.RemoveAsync(key);

            if (result.Success.HasValue && !result.Success.Value) return BadRequest(result);

            return Ok(result);
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

            if (result.Success.HasValue && !result.Success.Value) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("{key}/temperatures")]
        public async Task<IActionResult> GetCityByKeyWithTemperaturesAsync([FromRoute] Guid key)
        {
            var result = await CityService.GetCityByKeyWithTemperaturesAsync(key);

            if (result.Success.HasValue && !result.Success.Value) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("temperatures")]
        public async Task<IActionResult> GetAllTemperatureAsyncByCity()
        {
            var result = await CityService.GetAllWithTemperaturesAsync();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{key}/temperatures")]
        public async Task<IActionResult> RemoveTemperaturesAsync([FromRoute] Guid key)
        {
            var result = await CityService.RemoveTemperaturesByCityAsync(key);

            if (result.Success.HasValue && !result.Success.Value) return BadRequest(result);
            return Ok(result);
        }
    }
}