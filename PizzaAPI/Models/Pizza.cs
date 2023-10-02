namespace PizzaAPI.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }
        public string Size { get; set; } // Size of the pizza (small, medium, large)
        public List<Topping> Toppings { get; set; }
        public decimal TotalPrice { get; set; }

        // Constructor to initialize the pizza order
        public Pizza()
        {
            // Initialize the toppings listq
            Toppings = new List<Topping>();
        }

    }

}
