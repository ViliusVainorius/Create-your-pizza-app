using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("api/pizza-orders")]
    public class PizzaOrdersController : Controller
    {
        private readonly PizzaOrderAPIDbContext dbContext;

        public PizzaOrdersController(PizzaOrderAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzaOrders()
        {
            // Include the Toppings navigation property
            var pizzaOrders = await dbContext.PizzaOrders
                .Include(po => po.Toppings) // Assuming Toppings is the navigation property
                .ToListAsync();

            // Map the data to a DTO if needed
            var pizzaOrderDtos = pizzaOrders.Select(pizzaOrder => new
            {
                Id = pizzaOrder.Id,
                Size = pizzaOrder.Size,
                Toppings = pizzaOrder.Toppings.Select(topping => new
                {
                    Id = topping.Id,
                    Name = topping.Name
                }).ToList(), // Map toppings to a DTO if needed
                TotalPrice = pizzaOrder.TotalPrice
            });

            return Ok(pizzaOrderDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddPizzaOrder(AddPizzaRequest addPizzaRequest)
        {
            decimal totalPrice = 0.0m;

            decimal sizePrice = 0.0m;
            switch (addPizzaRequest.Size.ToLower())
            {
                case "small":
                    sizePrice = 8.0m;
                    break;
                case "medium":
                    sizePrice = 10.0m;
                    break;
                case "large":
                    sizePrice = 12.0m;
                    break;
                default:
                    sizePrice = 0.0m;
                    break;
            }

            decimal toppingPrice = addPizzaRequest.toppings.Count * 1.0m;

            totalPrice = sizePrice + toppingPrice;

            if (addPizzaRequest.toppings.Count > 3)
            {
                totalPrice *= 0.9m; // 10% discount
            }

            var pizza = new Pizza()
            {
                Id = Guid.NewGuid(),
                Size = addPizzaRequest.Size,
                Toppings = addPizzaRequest.toppings,
                TotalPrice = totalPrice
            };

            await dbContext.PizzaOrders.AddAsync(pizza);
            await dbContext.SaveChangesAsync();

            return Ok(pizza);
        }
    }
}
