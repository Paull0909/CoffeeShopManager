using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class OrderToppingDetails
    {
        public int OrderToppingDetailID { get; set; }
        public int ToppingID { get; set; }
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public Toppings Toppings { get; set; }
    }
}
