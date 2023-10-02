using Microsoft.EntityFrameworkCore;
using PizzaAPI.Models;

namespace PizzaAPI.Data
{
    public class SizeAPIDbContext : DbContext
    {
        public SizeAPIDbContext(DbContextOptions<SizeAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Size> Sizes { get; set; }

    }
}
