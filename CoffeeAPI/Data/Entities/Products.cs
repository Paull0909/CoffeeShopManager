using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Products
    {
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public string UrlImg { get; set; }
        public Categories_Products categories { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public List<ProductSizes> ProductSizes { get; set; }
        public List<Recipes> Recipes { get; set; }
    }
}
