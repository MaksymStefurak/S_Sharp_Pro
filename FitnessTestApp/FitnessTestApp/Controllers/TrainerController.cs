using FitnessTestApp.Service.DTOs;
using FitnessTestApp.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTestApp.Controllers
{
    [ApiController]
    [Route("api/trainers")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _service;

        public TrainerController(ITrainerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrainers()
        {
            var trainers = await _service.GetAllAsync();
            return Ok(trainers);
        }

        [HttpGet("{id:guid}", Name = nameof(GetTrainer))]
        public async Task<IActionResult> GetTrainer(Guid id)
        {
            var trainer = await _service.GetByIdAsync(id);
            if (trainer == null)
                return NotFound();

            return Ok(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainer([FromBody] TrainerDto dto)
        {
            if (dto == null)
                return BadRequest();

            await _service.AddAsync(dto);
            return CreatedAtRoute(nameof(GetTrainer), new { id = dto.Id }, dto);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTrainer(Guid id, [FromBody] TrainerDto dto)
        {
            if (dto == null || id != dto.Id)
                return BadRequest();

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateAsync(dto);
            return NoContent();
        }

 
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTrainer(Guid id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
