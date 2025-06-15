using AutoMapper;

namespace Data.DTO.Recipes
{
    public class RecipesViewModel
    {
        public int RecipeID { get; set; }
        public int ProductSizeID { get; set; }
        public int IngredientsID { get; set; }
        public float Quantity { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Recipes, RecipesViewModel>();
            }
        }
    }
}
