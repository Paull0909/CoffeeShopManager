namespace Data.Entities
{
    public class Recipes
    {
        public int RecipeID { get; set; }
        public int ProductSizeID { get; set; }
        public int IngredientsID { get; set; }
        public float Quantity { get; set; }       
        public ProductSizes ProductSizes { get; set; }
        public Ingredients Ingredients { get; set; }
    }
}
