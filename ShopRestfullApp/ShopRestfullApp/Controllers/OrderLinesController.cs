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
    public class OrderLinesController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public OrderLinesController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderLineDto>>> GetAll()
        {
            var orderLines = await _context.OrdersLines
                .Include(ol => ol.Product)
                .Include(ol => ol.Order)
                .ToListAsync();

            return Ok(orderLines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderLineDto>> GetById(int id)
        {
            var orderLine = await _context.OrdersLines
                .Include(ol => ol.Product)
                .Include(ol => ol.Order)
                .FirstOrDefaultAsync(ol => ol.Id == id);

            if (orderLine == null)
                return NotFound();

            return Ok(orderLine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderLine = await _context.OrdersLines.FindAsync(id);
            if (orderLine == null) return NotFound();

            _context.OrdersLines.Remove(orderLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
