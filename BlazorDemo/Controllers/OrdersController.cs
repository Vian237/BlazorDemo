using BlazorDemo.Data;
using BlazorDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaStoreContext _context;
        public OrdersController(PizzaStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderWithStatus>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Pizzas).ThenInclude(p => p.Special)
                .Include(o => o.Pizzas).ThenInclude(p => p.Toppings)
                .ThenInclude(pt => pt.Topping)
                .OrderByDescending(o => o.CreatedTime)
                .ToListAsync();

            return Ok(orders.Select(o => OrderWithStatus.FromOrder(o)).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<int>> PlaceOrder(Order order)
        {
            if (order == null || !order.Pizzas.Any())
            {
                return BadRequest("Order cannot be empty.");
            }

            order.CreatedTime = DateTime.Now;
            foreach (var pizza in order.Pizzas)
            {
                if (pizza.Special == null)
                {
                    return BadRequest("Pizza Special cannot be null.");
                }

                // Set the Special ID for the pizza
                pizza.SpecialId = pizza.Special.Id;
                pizza.Special = null; // Clear the navigation property to avoid circular references
            }
            // Add the order to the context
            _context.Orders.Add(order);
            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the ID of the newly created order
            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order.Id);
        }
    }
}
