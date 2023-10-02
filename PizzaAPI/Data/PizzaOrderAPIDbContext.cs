using Microsoft.EntityFrameworkCore;
using PizzaAPI.Models;

namespace PizzaAPI.Data
{
    public class PizzaOrderAPIDbContext : DbContext
    {
        public PizzaOrderAPIDbContext(DbContextOptions<PizzaOrderAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Pizza> PizzaOrders { get; set; }
    }
}
