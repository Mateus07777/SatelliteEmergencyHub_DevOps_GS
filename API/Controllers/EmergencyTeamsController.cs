using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.Services.Interfaces;

namespace SatelliteEmergencyHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmergencyTeamsController : ControllerBase
    {
        private readonly IEmergencyTeamService _service;

        public EmergencyTeamsController(IEmergencyTeamService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmergencyTeamRequest request)
        {
            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmergencyTeamRequest request) =>
            Ok(await _service.UpdateAsync(id, request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // Endpoint N:N: vincular equipe a uma ocorrência
        [HttpPost("{id:int}/occurrences")]
        public async Task<IActionResult> Assign(int id, [FromBody] AssignTeamToOccurrenceRequest request)
        {
            await _service.AssignToOccurrenceAsync(id, request);
            return NoContent();
        }

        // Endpoint N:N: desvincular equipe de uma ocorrência
        [HttpDelete("{id:int}/occurrences/{occurrenceId:int}")]
        public async Task<IActionResult> Unassign(int id, int occurrenceId)
        {
            await _service.UnassignFromOccurrenceAsync(id, occurrenceId);
            return NoContent();
        }
    }
}
