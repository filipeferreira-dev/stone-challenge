using Application.DTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
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
            return Ok(await CityService.AddCityAsync(postalCodeDto.PostalCode));
        }
    }
}