using FitnessTestApp.Service.DTOs;
using FitnessTestApp.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTestApp.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _service.GetAllAsync();
            return Ok(reservations);
        }

        [HttpGet("{id:guid}", Name = nameof(GetReservation))]
        public async Task<IActionResult> GetReservation(Guid id)
        {
            var reservation = await _service.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpGet("customer/{customerId:guid}")]
        public async Task<IActionResult> GetByCustomer(Guid customerId)
        {
            var reservations = await _service.GetByCustomerIdAsync(customerId);
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDto dto)
        {
            if (dto == null)
                return BadRequest();

            await _service.CreateAsync(dto);
            return CreatedAtRoute(nameof(GetReservation), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateReservation(Guid id, [FromBody] ReservationDto dto)
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
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
