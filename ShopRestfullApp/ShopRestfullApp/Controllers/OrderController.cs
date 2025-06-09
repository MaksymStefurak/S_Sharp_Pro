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
    public class OrderController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public OrderController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _context.Orders
                .Include(o=>o.Customer)
                .Include(o=>o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderLines)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(o=>o.Id==id);

            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto dto)
        {
            var order = new Order()
            {
                OrderData = dto.OrderDate,
                CustomerId = dto.CustomerId,
                OrderLines = dto.OrderLines.Select(ol => new OrderLine
                {
                    ProductId = ol.ProductId,
                    Quantity = ol.Quantity
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderDto dto)
        {
            var order = await _context.Orders.Include(o => o.OrderLines).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();

            order.OrderData = dto.OrderDate;
            order.CustomerId = dto.CustomerId;

            
            _context.OrdersLines.RemoveRange(order.OrderLines);
            order.OrderLines = dto.OrderLines.Select(ol => new OrderLine
            {
                ProductId = ol.ProductId,
                Quantity = ol.Quantity
            }).ToList();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderLines).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();

            _context.OrdersLines.RemoveRange(order.OrderLines);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
