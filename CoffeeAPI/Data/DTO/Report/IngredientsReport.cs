using Data.DTO.Recipes;

namespace Data.DTO.Report
{
    public class IngredientsReport
    {
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public List<ProductDetailReport> productDetails { get; set; }
    }
}
