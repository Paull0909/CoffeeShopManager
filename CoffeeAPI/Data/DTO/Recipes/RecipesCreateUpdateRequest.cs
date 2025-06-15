using AutoMapper;

namespace Data.DTO.Recipes
{
    public class RecipesCreateUpdateRequest
    {
        public int RecipeID { get; set; }
        public int ProductSizeID { get; set; }
        public int IngredientsID { get; set; }
        public float Quantity { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<RecipesCreateUpdateRequest, Entities.Recipes>();
            }
        }
    }
}
