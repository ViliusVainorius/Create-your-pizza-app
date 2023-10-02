using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("api/pizzas")]
    public class PizzasController : Controller
    {
        private readonly PizzaAPIDbContext dbContext;

        public PizzasController(PizzaAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzas()
        {
            return Ok(await dbContext.Pizzas.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddPizza(AddPizzaRequest addPizzaRequest)
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

            if(addPizzaRequest.toppings.Count > 3)
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

            await dbContext.Pizzas.AddAsync(pizza);
            await dbContext.SaveChangesAsync();

            return Ok(pizza);
        }

    }
}
