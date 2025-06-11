using CustomerTrainerService.DTOs;
using CustomerTrainerService.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerTrainerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAll()
        {
            var trainers = await _trainerService.GetAllAsync();
            return Ok(trainers);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TrainerDto>> GetById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            return Ok(trainer);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TrainerDto dto)
        {
            await _trainerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] TrainerDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch.");
            var success = await _trainerService.UpdateAsync(dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _trainerService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

