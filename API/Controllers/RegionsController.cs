using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.Services.Interfaces;

namespace SatelliteEmergencyHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class RegionsController : ControllerBase
    {
        private readonly IRegionService _service;

        public RegionsController(IRegionService service) 
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequest request)
        {
            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRegionRequest request)
            => Ok(await _service.UpdateAsync(id, request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
