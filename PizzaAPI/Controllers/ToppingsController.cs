using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("api/toppings")]
    public class ToppingsController : Controller
    {
        private readonly ToppingAPIDbContext dbContext;

        public ToppingsController(ToppingAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetToppings()
        {
            return Ok(await dbContext.Toppings.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddTopping(AddToppingRequest addToppingRequest)
        {
            var topping = new Topping()
            {
                Id = Guid.NewGuid(),
                Name = addToppingRequest.Name
            };

            await dbContext.Toppings.AddAsync(topping);
            await dbContext.SaveChangesAsync();

            return Ok(topping);
        }
    }
}
