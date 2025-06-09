using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopRestfullApp.Data.Context;
using ShopRestfullApp.Models.Dto_s;
using ShopRestfullApp.Models.Models;

namespace ShopRestfullApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public CustomersController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customer = await _context.Customers.ToListAsync();
            var customerDto = customer.Select(c => new CustomerDto
            {
                FullName = c.FullName,
                EmailAddress = c.EmailAddress
            }).ToList();
            return Ok(customerDto); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null) return NotFound();

            var dto = new CustomerDto
            {
                FullName = customer.FullName,
                EmailAddress = customer.EmailAddress
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CustomerDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                EmailAddress = dto.EmailAddress
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, dto); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            customer.FullName = dto.FullName;
            customer.EmailAddress = dto.EmailAddress;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
