using Microsoft.EntityFrameworkCore;
using PizzaAPI.Models;

namespace PizzaAPI.Data
{
    public class ToppingAPIDbContext : DbContext
    {
        public ToppingAPIDbContext(DbContextOptions<ToppingAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Topping> Toppings { get; set; }
    }
}
