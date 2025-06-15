using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ProductSizes
    {
        public int ProductSizeID { get; set; }
        public int ProductID { get; set; }
        [Required]
        public string SizeName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public Products Products { get; set; }
        public List<Recipes> Recipes { get; set; }
    }
}
