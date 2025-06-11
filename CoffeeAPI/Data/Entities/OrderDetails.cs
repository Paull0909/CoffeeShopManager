using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class OrderDetails
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        [Required]
        public int SizeID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Orders Orders { get; set; }
        public Products Products { get; set; }
        public List<OrderToppingDetails> OrderToppingDetails { get; set; }

    }
}
