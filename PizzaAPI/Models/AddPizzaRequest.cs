namespace PizzaAPI.Models
{
    public class AddPizzaRequest
    {
        public string Size { get; set; }
        public List<Topping> toppings { get; set; }
    }
}
