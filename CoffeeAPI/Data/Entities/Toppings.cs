using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Toppings
    {
        public int ToppingID { get; set; }
        [Required]
        public string ToppingName { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<OrderToppingDetails> OrderToppingDetails { get; set; }
    }
}
