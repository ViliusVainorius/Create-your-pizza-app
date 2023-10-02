using Microsoft.EntityFrameworkCore;
using PizzaAPI.Models;

namespace PizzaAPI.Data
{
    public class PizzaAPIDbContext : DbContext
    {
        public PizzaAPIDbContext(DbContextOptions<PizzaAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Pizza> Pizzas { get; set; }

    }
}
