using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("api/sizes")]
    public class SizesController : Controller
    {
        private readonly SizeAPIDbContext dbContext;

        public SizesController(SizeAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetSizes()
        {
            return Ok(await dbContext.Sizes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddSizes(AddSizeRequest addSizeRequest)
        {
            var size = new Size()
            {
                Id = Guid.NewGuid(),
                Name = addSizeRequest.Name,
                BasePrice = addSizeRequest.BasePrice
            };

            await dbContext.Sizes.AddAsync(size);
            await dbContext.SaveChangesAsync();

            return Ok(size);
        }
    }
}
